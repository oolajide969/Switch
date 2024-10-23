using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Logic;
using Trx.Messaging;
using Trx.Messaging.Channels;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583;

namespace Switcha.Processor.PeerConnection
{
    public class Listener
    {
        public void StartListener(SourceNode sourceNode)
        {
            TcpListener tcpListener = new TcpListener(sourceNode.Port);
            tcpListener.LocalInterface = sourceNode.IPAddress;
            tcpListener.Start();

            ListenerPeer listenerPeer = new ListenerPeer(sourceNode.ID.ToString(), new TwoBytesNboHeaderChannel
                (new Iso8583Ascii1987BinaryBitmapMessageFormatter(), sourceNode.IPAddress, sourceNode.Port),
                new BasicMessagesIdentifier(11, 41), tcpListener);

            listenerPeer.Connected += new PeerConnectedEventHandler(ListenerPeerConnected);
            listenerPeer.Receive += new PeerReceiveEventHandler(ListenerPeerReceive);
            listenerPeer.Disconnected += new PeerDisconnectedEventHandler(ListenerPeerDisconnected);
        }

        private void ListenerPeerConnected(object sender, EventArgs e)
        {
            ListenerPeer listenerPeer = sender as ListenerPeer;
            if (listenerPeer == null) return;
        }

        private void ListenerPeerReceive(object sender, ReceiveEventArgs e)
        {
            ListenerPeer sourcePeer = sender as ListenerPeer;

            //Get the ISO message
            Iso8583Message receivedMessage = e.Message as Iso8583Message;

            new TransactionLogLogic().LogTransaction(receivedMessage);

            if (receivedMessage == null) return;

            int sourceID = Convert.ToInt32(sourcePeer.Name);

            Iso8583Message responseMessage = new Processor().ProcessMessage(receivedMessage, sourceID);

            sourcePeer.Send(responseMessage);
            sourcePeer.Close();
            sourcePeer.Dispose();
        }

        private void ListenerPeerDisconnected(object sender, EventArgs e)
        {
            ListenerPeer listenerPeer = sender as ListenerPeer;
            if (listenerPeer == null) return;
            SourceNode sourceNode = new SuperEntityLogic<SourceNode>().GetByID(Convert.ToInt32(listenerPeer.Name));
            StartListener(sourceNode);
        }
    }
}
