using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Trx.Messaging.Iso8583;

namespace Switcha.Logic
{
    public class TransactionLogLogic : SuperEntityLogic<TransactionLogs>
    {
        public void LogTransaction(Iso8583Message message)
        {
            TransactionLogs transactionLog = new TransactionLogs(); 
            transactionLog.CardPAN = message.Fields[2].Value.ToString();
            if (message.IsRequest())
            {
                transactionLog.ResponseCode = "";
            }
            else
            {
                transactionLog.ResponseCode = message.Fields[39].Value.ToString();

            }
            transactionLog.Amount = Convert.ToDecimal(message.Fields[4].Value) / 100;
            transactionLog.Account1 = message.Fields[102].Value.ToString();
            if (message.Fields[103] != null)
            {
                transactionLog.Account2 = message.Fields[103].Value.ToString();
            }

            transactionLog.STAN = message.Fields[11].Value.ToString();
            transactionLog.TransactionDate = DateTime.Now;
            transactionLog.MTI = message.MessageTypeIdentifier.ToString();

            SuperEntityLogic<TransactionLogs> LogLogic = new SuperEntityLogic<TransactionLogs>();
            LogLogic.Insert(transactionLog);
            LogLogic.Commit();

        }
    }
}
