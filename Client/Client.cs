using System;
using System.IO;
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
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private BinaryFormatter formatter;
        public string clientName = "";

        public Client()
        {
            tcpClient = new TcpClient();
        }

        public bool Connect( string ipAddress, int port )
        {
            try
            {
                tcpClient.Connect( ipAddress, port );
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

                Thread thread = new Thread( () => { TcpProcessServerResponse(); } );
                thread.Start();

                clientForm.ShowDialog();
            }
            catch( Exception exception )
            {
                Console.WriteLine( "EXCEPTION: " + exception.Message );
            }
            finally
            {
                tcpClient.Close();
            }
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
                            string serverMessage = serverPacket.message;
                            clientForm.UpdateChatWindow( serverMessage, "left", Color.Purple, Color.White );
                            break;
                        case PacketType.CHAT_MESSAGE:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                            string chatMessage = chatPacket.message;
                            clientForm.UpdateChatWindow( chatMessage, "left", Color.Black, Color.PaleVioletRed );
                            break;
                        case PacketType.PRIVATE_MESSAGE:
                            break;
                        case PacketType.NICKNAME:
                            NicknamePacket namePacket = (NicknamePacket)packet;
                            clientName = namePacket.name;
                            break;
                    }
                }
            }
            catch( Exception exception )
            {
                Console.WriteLine( exception.Message );
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
    }
}