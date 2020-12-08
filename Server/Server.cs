using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    class Server
    {
        private int index;
        private UdpClient udpListener;
        private TcpListener tcpListerer;
        private List<string> clientNames;
        private bool keyReceived = false;
        private ConcurrentDictionary<int, Client> clients;

        public Server( string ipAddress, int port )
        {
            IPAddress localAddress = IPAddress.Parse( ipAddress );
            tcpListerer = new TcpListener( localAddress, port );
            udpListener = new UdpClient( port );
            clientNames = new List<string>();
            clientNames.Add( "Initial" );
        }

        public void Start()
        {
            clients = new ConcurrentDictionary<int, Client>();
            tcpListerer.Start();

            int clientID = 0;
            while( true )
            {
                index = clientID;
                clientID++;

                Socket socket = tcpListerer.AcceptSocket();

                Client client = new Client( socket );
                clients.TryAdd( index, client );

                Thread tcpThread = new Thread( () => { TcpClientMethod( client ); } );
                tcpThread.Start();
                
                Thread udpThread = new Thread( () => { UdpListen(); } );
                udpThread.Start();
            }
        }

        public void Stop()
        {
            tcpListerer.Stop();
        }

        private void TcpClientMethod( Client client )
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
                            case PacketType.CHAT_MESSAGE:
                                ChatMessagePacket inChatPacket = (ChatMessagePacket)packet;
                                ChatMessagePacket outChatPacket = new ChatMessagePacket( "[" + client.name + "]: " + inChatPacket.message );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    if ( c.Value != client )
                                        c.Value.TcpSend( outChatPacket );
                                break;
                            case PacketType.PRIVATE_MESSAGE:
                                PrivateMessagePacket inPrivatePacket = (PrivateMessagePacket)packet;
                                PrivateMessagePacket outPrivatePacket = new PrivateMessagePacket( inPrivatePacket.message, inPrivatePacket.name );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    if ( c.Value.name == outPrivatePacket.name )
                                        c.Value.TcpSend( outPrivatePacket );
                                break;
                            case PacketType.ENCRYPTED_MESSAGE:
                                EncryptedMessagePacket encryptedMessage = (EncryptedMessagePacket)packet;
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    if ( c.Value != client )
                                        c.Value.TcpSend( encryptedMessage );
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
                                if ( !clientListPacket.removeText )
                                    clientNames.Add( clientListPacket.name );
                                else if ( clientListPacket.removeText )
                                    clientNames.Remove( clientListPacket.name );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                {
                                    for ( int i = 0; i < clientNames.Count; i++ )
                                    {
                                        if ( i == 0 )
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], true ) );
                                        else
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], false ) );
                                    }
                                }
                                break;
                            case PacketType.LOGIN:
                                LoginPacket loginPacket = (LoginPacket)packet;
                                clients[index - 1].endPoint = loginPacket.EndPoint;
                                if ( !keyReceived )
                                {
                                    keyReceived = true;
                                    client.PublicKey = loginPacket.PublicKey;
                                }
                                foreach ( KeyValuePair<int, Client> c in clients )
                                {
                                    c.Value.TcpSend( new LoginPacket( null, client.PublicKey ) );
                                    for ( int i = 0; i < clientNames.Count; i++ )
                                    {
                                        if ( i == 0 )
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], true ) );
                                        else
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], false ) );
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch ( Exception exception )
            {
                Console.WriteLine( "Server TCP Read Method Exception: " + exception.Message );
            }
            finally
            {
                client.Close();
                clients.TryRemove( index, out client );
            }
        }

        private void UdpListen()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint( IPAddress.Any, 0 );
                while( true )
                {
                    byte[] bytes = udpListener.Receive( ref endPoint );
                    MemoryStream memoryStream = new MemoryStream( bytes );
                    Packet packet = new BinaryFormatter().Deserialize( memoryStream ) as Packet;
                    foreach( KeyValuePair<int, Client> c in clients )
                    {
                        if ( endPoint.ToString() != c.Value.endPoint.ToString() )
                        {
                            switch ( packet.packetType )
                            {
                                case PacketType.CHAT_MESSAGE:
                                    ChatMessagePacket inChatPacket = (ChatMessagePacket)packet;
                                    ChatMessagePacket outChatPacket = new ChatMessagePacket( "[" + c.Value.name + "]: " + inChatPacket.message );
                                    UdpSend( c.Value, outChatPacket );
                                    break;
                                case PacketType.PRIVATE_MESSAGE:
                                    PrivateMessagePacket inPrivatePacket = (PrivateMessagePacket)packet;
                                    PrivateMessagePacket outPrivatePacket = new PrivateMessagePacket( inPrivatePacket.message, inPrivatePacket.name );
                                    if ( c.Value.name == outPrivatePacket.name )
                                        UdpSend( c.Value, outPrivatePacket );
                                    break;
                                case PacketType.ENCRYPTED_MESSAGE:
                                    EncryptedMessagePacket encryptedMessage = (EncryptedMessagePacket)packet;
                                    UdpSend( c.Value, encryptedMessage );
                                    break;
                            }
                        }
                    }
                }
            }
            catch( SocketException e )
            {
                Console.WriteLine( "Server UDP Read Method Exception: " + e.Message );
            }
        }

        private void UdpSend( Client client, Packet packet )
        {
            MemoryStream memoryStream = new MemoryStream();
            new BinaryFormatter().Serialize( memoryStream, packet );
            byte[] buffer = memoryStream.GetBuffer();
            udpListener.Send( buffer, buffer.Length, client.endPoint );
            memoryStream.Close();
        }
    }
}