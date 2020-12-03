using System;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public class Client
    {
        public ClientForm clientForm;
        private TcpClient tcpClient;
        private UdpClient udpClient;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private BinaryFormatter formatter;
        public string clientName = "";

        public Client()
        {
            tcpClient = new TcpClient();
            udpClient = new UdpClient();
        }

        public bool Connect( string ipAddress, int port )
        {
            try
            {
                tcpClient.Connect( ipAddress, port );
                udpClient.Connect( ipAddress, port );
                stream = tcpClient.GetStream();
                reader = new BinaryReader( stream, Encoding.UTF8 );
                writer = new BinaryWriter( stream, Encoding.UTF8 );
                formatter = new BinaryFormatter();
                return true;
            }
            catch( Exception exception )
            {
                Console.WriteLine( "Client Exception: " + exception.Message );
                return false;
            }
        }

        public void Run()
        {
            try
            {
                clientForm = new ClientForm( this );

                Thread tcpThread = new Thread( () => { TcpProcessServerResponse(); } );
                tcpThread.Start();

                Thread udpThread = new Thread( () => { UdpProcessServerResponse(); } );
                udpThread.Start();
                Login();

                try
                {
                    clientForm.ShowDialog();
                }
                catch ( Exception exception )
                {
                    Console.WriteLine( exception.Message );
                }
            }
            catch( Exception exception )
            {
                Console.WriteLine( "EXCEPTION: " + exception.Message );
            }
            finally
            {
                tcpClient.Close();
                udpClient.Close();
            }
        }

        public void Login()
        {
            TcpSendMessage( new LoginPacket( (IPEndPoint)udpClient.Client.LocalEndPoint ) );
        }

        private void TcpProcessServerResponse()
        {
            try
            {
                int numberOfBytes = 0;
                while( ( numberOfBytes = reader.ReadInt32() ) != -1 )
                {
                    byte[] buffer = reader.ReadBytes( numberOfBytes );
                    MemoryStream memoryStream = new MemoryStream( buffer );
                    Packet packet = formatter.Deserialize( memoryStream ) as Packet;
                    switch ( packet.packetType )
                    {
                        case PacketType.EMPTY:
                            break;
                        case PacketType.SERVER_MESSAGE:
                            ServerMessagePacket serverPacket = (ServerMessagePacket)packet;
                            clientForm.UpdateChatWindow( serverPacket.message, "left", Color.Purple, Color.White );
                            break;
                        case PacketType.CHAT_MESSAGE:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                            clientForm.UpdateChatWindow( chatPacket.message, "left", Color.Black, Color.PaleVioletRed );
                            break;
                        case PacketType.PRIVATE_MESSAGE:
                            PrivateMessagePacket privatePacket = (PrivateMessagePacket)packet;
                            clientForm.UpdateChatWindow( privatePacket.message, "left", Color.Black, Color.LightYellow );
                            break;
                        case PacketType.NICKNAME:
                            NicknamePacket namePacket = (NicknamePacket)packet;
                            clientName = namePacket.name;
                            break;
                        case PacketType.CLIENT_LIST:
                            ClientListPacket clientListPacket = (ClientListPacket)packet;
                            clientForm.UpdateClientList( clientListPacket.name, clientListPacket.removeText );
                            break;
                    }
                }
            }
            catch( Exception exception )
            {
                Console.WriteLine( exception.Message );
            }
        }

        private void UdpProcessServerResponse()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint( IPAddress.Any, 0 );
                while( true )
                {
                    byte[] bytes = udpClient.Receive( ref endPoint );
                    MemoryStream memoryStream = new MemoryStream( bytes );
                    Packet packet = formatter.Deserialize( memoryStream ) as Packet;
                    switch( packet.packetType )
                    {
                        case PacketType.LOGIN:
                            clientForm.UpdateChatWindow( "This is a LOGIN packet.", "left", Color.Black, Color.White );
                            break;
                    }
                }
            }
            catch( SocketException e )
            {
                Console.WriteLine( "Client UDP Read Method Exception: " + e.Message );
            }
        }

        public void TcpSendMessage( Packet message )
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize( memoryStream, message );
            byte[] buffer = memoryStream.GetBuffer();
            writer.Write( buffer.Length );
            writer.Write( buffer );
            writer.Flush();
            memoryStream.Close();
        }

        public void UdpSendMessage( Packet message )
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize( memoryStream, message );
            byte[] buffer = memoryStream.GetBuffer();
            udpClient.Send( buffer, buffer.Length );
            memoryStream.Close();
        }
    }
}