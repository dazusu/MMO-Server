using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MMO.Entities;
using MMO.Packets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketMessaging;
using SocketMessaging.Server;

namespace MMO.Networking
{
    public static class Server
    {
        private static int _port;
        private static TcpServer _server;
        public delegate void ConnectionHandler(Connection connection);
        public static event ConnectionHandler Connection;

        public delegate void PacketHandler(PacketContainer packetContainer);
        public static event PacketHandler ClientPacket;

        public static void Start(int port)
        {
            _port = port;
            _server = new TcpServer();
            _server.Start(_port);
            _server.Connected += _server_Connected;
            Debug.WriteInfo("Server started!");
            Debug.WriteInfo($"Now accepting connections on {_port}");
        }

        private static void _server_Connected(object sender, ConnectionEventArgs e)
        {
            // hook event for recieving message for this socket
            e.Connection.SetMode(MessageMode.DelimiterBound);
            e.Connection.SetDelimiter(new byte[] {13,10});
            e.Connection.ReceivedMessage += Connection_ReceivedMessage;
            Connection(e.Connection);
        }

        /// <summary>
        /// Read data from the client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Connection_ReceivedMessage(object sender, EventArgs e)
        {
            Connection connection = sender as SocketMessaging.Connection;

            IPEndPoint ipEndPoint = connection.Socket.RemoteEndPoint as IPEndPoint;

            // Receive data.
            string v = connection.ReceiveMessageString();

            // Find out which packet we're recieving
            dynamic dynamicPacket = JObject.Parse(v);
            PacketType type = dynamicPacket.PacketType;

            PacketContainer container = new PacketContainer()
            {
                PacketType = type,
                ConnectionId = connection.Id,
                IpAddress = ipEndPoint.Address.ToString()
            };

            switch (type)
            {
                case PacketType.Login: container.Packet = JObject.Parse(v).ToObject<LoginPacket>();
                    break;
                case PacketType.MoveToDest: container.Packet = JObject.Parse(v).ToObject<MoveToDestPacket>();
                    break;
                case PacketType.Move:
                    container.Packet = JObject.Parse(v).ToObject<MovePacket>();
                    break;
            }

            ClientPacket?.Invoke(container);
        }

        public static void PushPacket(BulkPacket bulk)
        {
            if (!bulk.Packets.Any())
                return;

            string p = JsonConvert.SerializeObject(bulk.Packets);

            Debug.WritePacket($"Sending {bulk.Packets.Count} packets to {bulk.TargetConnectionIds.Length} clients.");

            foreach (int id in bulk.TargetConnectionIds)
            {
                _server.Connections.Single(x => x.Id == id).Send(p);
            }

        }

        public static void PushPacket(List<BulkPacket> bulks)
        {
            foreach (BulkPacket bulk in bulks)
            {
                string p = JsonConvert.SerializeObject(bulk.Packets);

                Debug.WritePacket($"Sending {bulk.Packets.Count} packets to {bulk.TargetConnectionIds.Length} clients.");

                foreach(int id in bulk.TargetConnectionIds)
                {
                    _server.Connections.Single(x => x.Id == id).Send(p);
                }
            }
        }
    }

    public class PacketContainer
    {
        public string IpAddress { get; set; }
        public int ConnectionId { get; set; }
        public IPacket Packet { get; set; }
        public PacketType PacketType { get; set; }

        public PacketContainer()
        {
            Packet = null;
        }
    }
}
