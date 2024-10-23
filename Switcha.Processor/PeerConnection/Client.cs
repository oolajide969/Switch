using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Trx.Messaging.Channels;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583;

namespace Switcha.Processor.PeerConnection
{
    public class Client
    {
        public ClientPeer StartClient(SinkNode sinkNode)
        {
            ClientPeer clientPeer = new ClientPeer(sinkNode.Name, new TwoBytesNboHeaderChannel(
                    new Iso8583Ascii1987BinaryBitmapMessageFormatter(), sinkNode.IPAddress, sinkNode.Port),
                    new Trx.Messaging.BasicMessagesIdentifier(11, 41));

            //clientPeer.Connect();

            clientPeer.RequestDone += new PeerRequestDoneEventHandler(ClientRequestDone);
            clientPeer.RequestCancelled += new PeerRequestCancelledEventHandler(ClientRequestCancelled);

            clientPeer.Connected += new PeerConnectedEventHandler(ClientPeerConnected);
            clientPeer.Receive += new PeerReceiveEventHandler(ClientPeerOnReceive);
            clientPeer.Disconnected += new PeerDisconnectedEventHandler(ClientPeerDisconnected);

            return clientPeer;
        }

        public static void ClientRequestDone(object sender, PeerRequestDoneEventArgs e)
        {
            Iso8583Message response = e.Request.RequestMessage as Iso8583Message;
            SourceNode source = e.Request.Payload as SourceNode;
        }

        public static void ClientRequestCancelled(object sender, PeerRequestCancelledEventArgs e)
        {
            Iso8583Message response = e.Request.RequestMessage as Iso8583Message;
            SourceNode source = e.Request.Payload as SourceNode;
        }


        private void ClientPeerConnected(object sender, EventArgs e)
        {
            ClientPeer client = sender as ClientPeer;
            if (client == null) return;
        }

        private void ClientPeerOnReceive(object sender, ReceiveEventArgs e)
        {
            ClientPeer clientPeer = sender as ClientPeer;
            Iso8583Message receivedMessage = e.Message as Iso8583Message;
        }

        private void ClientPeerDisconnected(object sender, EventArgs e)
        {
            ClientPeer client = sender as ClientPeer;
            if (client == null) return;
        }
    }
}
