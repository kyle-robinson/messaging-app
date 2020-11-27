using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Server
{
    class Server
    {
        private TcpListener tcpListerer;
        private ConcurrentBag<Client> clients;

        public Server( string ipAddress, int port )
        {
            IPAddress localAddress = IPAddress.Parse( ipAddress );
            tcpListerer = new TcpListener( localAddress, port );
        }

        public void Start()
        {
            clients = new ConcurrentBag<Client>();
            tcpListerer.Start();
            while( true )
            {
                Socket socket = tcpListerer.AcceptSocket();
                Client client = new Client( socket );
                clients.Add( client );

                Thread thread = new Thread( () => { ClientMethod( client ); } );
                thread.Start();
            }
        }

        public void Stop()
        {
            tcpListerer.Stop();
        }

        private void ClientMethod( Client client )
        {
            try
            {
                Packet packet;
                while ( ( packet = client.TcpRead() ) != null )
                {
                    if ( packet != null )
                    {
                        Console.WriteLine( "Received" );
                        switch ( packet.packetType )
                        {
                            case PacketType.EMPTY:
                                break;
                            case PacketType.SERVER_MESSAGE:
                                ServerMessagePacket inServerPacket = (ServerMessagePacket)packet;
                                ServerMessagePacket outServerPacket = new ServerMessagePacket( inServerPacket.message );
                                foreach ( Client c in clients )
                                    if ( c != client )
                                        c.TcpSend( outServerPacket );
                                break;
                            case PacketType.CHAT_MESSAGE:
                                ChatMessagePacket inChatPacket = (ChatMessagePacket)packet;
                                ChatMessagePacket outChatPacket = new ChatMessagePacket( client.name + ": " + inChatPacket.message );
                                foreach ( Client c in clients )
                                    if ( c != client )
                                        c.TcpSend( outChatPacket );
                                break;
                            case PacketType.PRIVATE_MESSAGE:
                                break;
                            case PacketType.NICKNAME:
                                NicknamePacket namePacket = (NicknamePacket)packet;
                                client.name = namePacket.name;
                                if ( client.name != "" )
                                    client.TcpSend( new NicknamePacket( client.name ) );
                                else
                                    client.TcpSend( new NicknamePacket( null ) );
                                break;
                            case PacketType.CLIENT_LIST:
                                ClientListPacket clientListPacket = (ClientListPacket)packet;
                                client.name = clientListPacket.name;
                                foreach ( Client c in clients )
                                {
                                    if ( !clientListPacket.removeText )
                                        c.TcpSend( new ClientListPacket( client.name, false ) );
                                    else if ( clientListPacket.removeText )
                                        c.TcpSend( new ClientListPacket( client.name, true ) );
                                }
                                break;
                        }
                    }
                }
            }
            catch ( Exception exception )
            {
                Console.WriteLine( "EXCEPTION: " + exception.Message );
            }
            finally
            {
                client.Close();
                clients.TryTake( out client );
            }
        }
    }
}