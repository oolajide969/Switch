using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Switcha.Core.Models; 
using Switcha.Logic;
using Switcha.Processor.PeerConnection;
using Trx.Messaging;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583;

namespace Switcha.Processor
{
    public class Processor
    {
        public Iso8583Message ProcessMessage(Iso8583Message message, int sourceID)
        {
            if (message.MessageTypeIdentifier != 420)
            {
                message = AddDataElement(message);
            }
            SourceNode sourceNode = new SuperEntityLogic<SourceNode>().GetByID(sourceID);
            bool returnMsg;
            message = CheckMessageType(message, sourceNode, out returnMsg);

            if (returnMsg == true)
            {
                return message;
            }

            Iso8583Message responseMessage;
            string expiryDate = message.Fields[14].Value.ToString();
            DateTime cardExpiry = ParseDate(expiryDate);

            //expired card

            if (cardExpiry < DateTime.Now)
            {
                responseMessage = SetResponseMessage(message, "54");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            string transactionTypeCode = message.Fields[3].Value.ToString().Substring(0, 2);
            decimal amount = ConvertIsoAmountToDecimal(message.Fields[4].Value.ToString());
            if (transactionTypeCode != "31" && amount <= 0)
            {
                responseMessage = SetResponseMessage(message, "13");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            string cardBIN = message.Fields[2].Value.ToString().Substring(0, 6);
            Route route = new RouteLogic().GetRouteUsingBIN(cardBIN);

            if (route == null)
            {
                responseMessage = SetResponseMessage(message, "15");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            if (route.sinkNode == null || route.sinkNode.Status == Status.Inactive)
            {
                Console.WriteLine("Sink Node is null");
                responseMessage = SetResponseMessage(message, "91");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            Scheme scheme;
             
            try
            {
                scheme = sourceNode.Schemes.Where(x => x.Route == route).SingleOrDefault();
            }
            catch (Exception)
            {
                responseMessage = SetResponseMessage(message, "06");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            if (scheme == null)
            {
                responseMessage = SetResponseMessage(message, "58");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }


            TransactionType transactionType = new TransactionTypeLogic().GetTransactionTypeUsingCode(transactionTypeCode);
            string channelCode = message.Fields[41].Value.ToString().Substring(0, 1);
            Channels channel = new ChannelsLogic().GetChannelUsingCode(channelCode);

            Fee fee = GetFee(transactionType, channel, scheme);

            if (fee == null)
            {
                responseMessage = SetResponseMessage(message, "59");
                new TransactionLogLogic().LogTransaction(responseMessage);
                return responseMessage;
            }

            decimal fees = Calculate_Fee(fee, amount);

            message = Set_Fee(message, fees);

            //checks and balances done

            bool needReversal = false;
            responseMessage = ToDestination(message, route.sinkNode, out needReversal);


            return responseMessage;
        }

        private Iso8583Message CheckMessageType(Iso8583Message originalMessage, SourceNode sourceNode, out bool returnMessage)
        {
            returnMessage = false;
            return originalMessage;
        }

        private Iso8583Message AddDataElement(Iso8583Message message)
        {
            DateTime transmissionDate = DateTime.UtcNow;
            string transactionDate = string.Format("{0}{1}",
                    string.Format("{0:00}{1:00}", transmissionDate.Month, transmissionDate.Day),
                    string.Format("{0:00}{1:00}{2:00}", transmissionDate.Hour,
                    transmissionDate.Minute, transmissionDate.Second));

            string originalDataElement = string.Format("{0:0000}{1:000000}{2:MMddHHmmss}{3:00000000000}{4:00000000000}", message.MessageTypeIdentifier.ToString().PadLeft(4, '0'),
                 message.Fields[11].ToString().PadLeft(6, '0'), transactionDate, message.Fields[32].ToString().PadLeft(11, '0'), message.Fields[33].ToString().PadLeft(11, '0'));
            message.Fields.Add(90, originalDataElement);

            return message;
        }

        private DateTime ParseDate(string date)
        {
            string year = date.Substring(0, 2);
            string month = date.Substring(2, 2);

            string expiry = "30" + "-" + month + "-"  + year;

            //MM-DD-YY
            DateTime cardExpiryDate;

            if (DateTime.TryParse(expiry, out cardExpiryDate))
            {
                //is a date
                return cardExpiryDate;
            }
            else
            {
                //not a valid date
                return DateTime.Now;
            }
        }

        private Iso8583Message SetResponseMessage(Iso8583Message message, string responseCode)
        {
            message.SetResponseMessageTypeIdentifier();
            message.Fields.Add(39, responseCode);
            return message;
        }

        private decimal ConvertIsoAmountToDecimal(string amountIsoFormat)
        {
            decimal amount = Convert.ToDecimal(amountIsoFormat) / 100;
            return amount;
        }

        private Fee GetFee(TransactionType transactionType, Channels channel, Scheme scheme)
        {
            foreach (Combo combo in scheme.Combos)
            {
                if (transactionType == combo.TransactionType && channel == combo.Channel)
                {
                    return combo.Fee;
                }
            }
            return null; //Fee not Found
        }

        private decimal Calculate_Fee(Fee fee, decimal amount)
        {
            if(fee.FeeOptions == FeeEnum.FlatAmount)
            {
                decimal fees = fee.Amount;
                return fees;
            }
            else if (fee.FeeOptions == FeeEnum.Percentage)
            {
                decimal fees = (fee.Amount / 100) * amount;

                if (fees < fee.Minimum)
                {
                    fees = fee.Minimum;
                }
                else if (fees > fee.Maximum)
                {
                    fees = fee.Maximum;
                }
                else
                {
                    fees = 0;
                }
                return fees;
            }
            else
            {
                return 0;
            }
        }
        private Iso8583Message Set_Fee(Iso8583Message message, decimal fee)
        {
            string feeAmount = ConvertFeeToISOFormat(fee);
            message.Fields.Add(28, feeAmount);
            return message;
        }

        private string ConvertFeeToISOFormat(decimal fee)
        {
            decimal feeInMinorDenomination = fee * 100; //in Kobo

            StringBuilder feeStringBuilder = new StringBuilder(Convert.ToInt32(feeInMinorDenomination).ToString());
            string padded = feeStringBuilder.ToString().PadLeft(8, '0');
            feeStringBuilder.Replace(feeStringBuilder.ToString(), padded);
            feeStringBuilder.Insert(0, 'C');

            return feeStringBuilder.ToString();
        }

        private Iso8583Message ToDestination(Iso8583Message message, SinkNode sinknode, out bool needReversal)
        {
            Message response = null;
            needReversal = false;
            try
            {
                if (message == null)
                {
                    return SetResponseMessage(message, "20");
                }
                if (sinknode == null)
                {
                    Console.WriteLine("sink Node is null");
                    return SetResponseMessage(message, "91");
                }
                if (sinknode.Status == Status.Inactive)
                {
                    Console.WriteLine("Sink Node is Inactive");
                    return SetResponseMessage(message, "91");
                }
                int maxNumberOfEntries = 3;
                int serverTimeOut = 60000;


                ClientPeer clientPeer = new Client().StartClient(sinknode);

                int retries = 0;
                while (retries < maxNumberOfEntries)
                {
                    if (clientPeer.IsConnected)
                    {
                        break;
                    }
                    else
                    {
                        retries++;
                        clientPeer.Connect();
                    }
                    Thread.Sleep(5000);


                }

                PeerRequest request = null;
                if (clientPeer.IsConnected)
                {
                    request = new PeerRequest(clientPeer, message);
                    request.Send();
                    request.WaitResponse(serverTimeOut);

                    if (request.Expired)
                    {
                        needReversal = true;
                        return SetResponseMessage(message, "68");  
                    }
                    if (request != null)
                    {
                        response = request.ResponseMessage;

                    }

                    clientPeer.Close();
                    return response as Iso8583Message;

                }
                else
                {
                    Console.WriteLine("Client Peer is not Connected");
                    return SetResponseMessage(message, "91");
                }
            }
            catch (Exception)
            {
                return SetResponseMessage(message, "06");
            }
        }
    }
}
