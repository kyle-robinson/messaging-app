﻿using System;
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
        private List<string> gameOptions;
        private List<string> clientNames;
        private bool adminIsConnected = false;
        private bool gameStarted = false;
        private string choiceToWin;
        private int index;
        private UdpClient udpListener;
        private TcpListener tcpListerer;
        private ConcurrentDictionary<int, Client> clients;

        public Server( string ipAddress, int port )
        {
            gameOptions = new List<string> { "rock", "paper", "scissors" };
            IPAddress localAddress = IPAddress.Parse( ipAddress );
            tcpListerer = new TcpListener( localAddress, port );
            udpListener = new UdpClient( port );
            clientNames = new List<string>();
            clientNames.Add( "Empty" );
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
            udpListener.Close();
        }

        private void StartNewGame()
        {
            Random random = new Random();
            int gameIndex = random.Next( gameOptions.Count );
            choiceToWin = gameOptions[gameIndex];
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
                            case PacketType.LOGIN:
                                LoginPacket loginPacket = (LoginPacket)packet;
                                clients[index - 1].endPoint = loginPacket.EndPoint;
                                clients[index - 1].ClientKey = loginPacket.PublicKey;
                                client.TcpSend( new LoginPacket( null, client.PublicKey ) );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                {
                                    for ( int i = 0; i < clientNames.Count; i++ )
                                    {
                                        if ( i == 0 )
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], true ) );
                                        else
                                            c.Value.TcpSend( new ClientListPacket( clientNames[i], false ) );
                                    }
                                    c.Value.TcpSend( new AdminPacket( adminIsConnected ) );
                                }
                                break;
                            case PacketType.ENCRYPTED_PRIVATE_MESSAGE:
                                EncryptedPrivateMessagePacket inPrivatePacket = (EncryptedPrivateMessagePacket)packet;
                                string inPrivateMessage = client.DecryptString( inPrivatePacket.message );
                                string inPrivateName = client.DecryptString( inPrivatePacket.name );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    if ( c.Value.name == inPrivateName )
                                        c.Value.TcpSend( new EncryptedPrivateMessagePacket( c.Value.EncryptString( inPrivateMessage ), null ) );
                                break;
                            case PacketType.ENCRYPTED_MESSAGE:
                                EncryptedMessagePacket encryptedPacket = (EncryptedMessagePacket)packet;
                                string decryptedMessage = client.DecryptString( encryptedPacket.message );
                                foreach ( KeyValuePair<int, Client> c in clients )
                                {
                                    if ( c.Value != client )
                                    {
                                        byte[] encryptedMessage = c.Value.EncryptString( "[" + client.name + "]: " + decryptedMessage );
                                        c.Value.TcpSend( new EncryptedMessagePacket( encryptedMessage ) );
                                    }
                                }
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
                            case PacketType.ADMIN:
                                AdminPacket adminPacket = (AdminPacket)packet;
                                adminIsConnected = adminPacket.adminConnected;
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    c.Value.TcpSend( adminPacket );
                                break;
                            case PacketType.GLOBAL_MUTE:
                                GlobalMutePacket mutePacket = (GlobalMutePacket)packet;
                                foreach ( KeyValuePair<int, Client> c in clients )
                                    c.Value.TcpSend( mutePacket );
                                break;
                            case PacketType.ENCRYPTED_GAME:
                                EncryptedGamePacket gamePacket = (EncryptedGamePacket)packet;
                                string userGuess = client.DecryptString( gamePacket.userGuess );
                                if ( gameStarted )
                                {
                                    if ( userGuess.Equals( choiceToWin, StringComparison.InvariantCultureIgnoreCase ) )
                                    {
                                        gameStarted = false;
                                        client.TcpSend( new EncryptedServerPacket( client.EncryptString( "Correct! You have won the game!\nEnter '/game start' to start new game." ) ) );
                                        foreach ( KeyValuePair<int, Client> c in clients )
                                            if ( c.Value != client )
                                                c.Value.TcpSend( new EncryptedServerPacket( c.Value.EncryptString( client.name + " has won the game!" ) ) );
                                    }
                                    else
                                        client.TcpSend( new EncryptedServerPacket( client.EncryptString( "Incorrect! Keep guessing." ) ) );
                                }
                                else if ( userGuess.Equals( "start", StringComparison.InvariantCultureIgnoreCase ) )
                                {
                                    StartNewGame();
                                    gameStarted = true;
                                    client.TcpSend( new EncryptedServerPacket( client.EncryptString( "You have started a new game.\nChoose from [rock], [paper] [scissors]." ) ) );
                                    foreach ( KeyValuePair<int, Client> c in clients )
                                        if ( c.Value != client )
                                            c.Value.TcpSend( new EncryptedServerPacket( c.Value.EncryptString( client.name + " has started a new game.\nChoose from [rock], [paper] [scissors].") ) );
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
                clients[index - 1].Close();
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