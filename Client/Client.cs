using System;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public class Client
    {
        public string clientName = "";
        private ClientForm clientForm;
        private TcpClient tcpClient;
        private UdpClient udpClient;
        private BinaryReader reader;
        private BinaryWriter writer;
        private NetworkStream stream;
        private BinaryFormatter formatter;
        private RSACryptoServiceProvider RSAProvider;
        private RSAParameters ServerKey;
        private RSAParameters PublicKey;
        private RSAParameters PrivateKey;

        public Client()
        {
            tcpClient = new TcpClient();
            udpClient = new UdpClient();
            RSAProvider = new RSACryptoServiceProvider( 2048 );
            PublicKey = RSAProvider.ExportParameters( false );
            PrivateKey = RSAProvider.ExportParameters( true );
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
                Console.WriteLine( "Client Connect Exception: " + exception.Message );
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

                TcpSendMessage( new LoginPacket( (IPEndPoint)udpClient.Client.LocalEndPoint, PublicKey ) );

                clientForm.ShowDialog();
            }
            catch( Exception exception )
            {
                Console.WriteLine( "Client Run Exception: " + exception.Message );
            }
            finally
            {
                tcpClient.Close();
                udpClient.Close();
            }
        }

        private void TcpProcessServerResponse()
        {
            try
            {
                int numberOfBytes = 0;
                while ( ( numberOfBytes = reader.ReadInt32() ) != -1 )
                {
                    byte[] buffer = reader.ReadBytes( numberOfBytes );
                    MemoryStream memoryStream = new MemoryStream( buffer );
                    Packet packet = formatter.Deserialize( memoryStream ) as Packet;
                    switch ( packet.packetType )
                    {
                        case PacketType.ENCRYPTED_SERVER:
                            EncryptedServerPacket serverPacket = (EncryptedServerPacket)packet;
                            clientForm.UpdateCommandWindow( DecryptString( serverPacket.message ), Color.Black, Color.MediumPurple );
                            break;
                        case PacketType.LOGIN:
                            LoginPacket loginPacket = (LoginPacket)packet;
                            ServerKey = loginPacket.PublicKey;
                            break;
                        case PacketType.ENCRYPTED_PRIVATE_MESSAGE:
                            EncryptedPrivateMessagePacket privatePacket = (EncryptedPrivateMessagePacket)packet;
                            clientForm.UpdateChatWindow( DecryptString( privatePacket.message ), "left", Color.Black, Color.LightPink );
                            break;
                        case PacketType.ENCRYPTED_MESSAGE:
                            EncryptedMessagePacket encryptedPacket = (EncryptedMessagePacket)packet;
                            clientForm.UpdateChatWindow( DecryptString( encryptedPacket.message ), "left", Color.Black, Color.MediumPurple );
                            break;
                        case PacketType.NICKNAME:
                            NicknamePacket namePacket = (NicknamePacket)packet;
                            clientName = namePacket.name;
                            break;
                        case PacketType.CLIENT_LIST:
                            ClientListPacket clientListPacket = (ClientListPacket)packet;
                            clientForm.UpdateClientList( clientListPacket.name, clientListPacket.removeText );
                            break;
                        case PacketType.ADMIN:
                            AdminPacket adminPacket = (AdminPacket)packet;
                            clientForm.adminConnected = adminPacket.adminConnected;
                            break;
                        case PacketType.GLOBAL_MUTE:
                            GlobalMutePacket mutePacket = (GlobalMutePacket)packet;
                            if ( clientForm.mutedClientsGlobal.Contains( mutePacket.clientToMute ) )
                            {
                                clientForm.mutedClientsGlobal.Remove( mutePacket.clientToMute );
                                if ( mutePacket.clientToMute == clientName )
                                    clientForm.UpdateCommandWindow( "You have been unmuted by the Admin.", Color.Black, Color.SkyBlue );
                            }
                            else
                            {
                                clientForm.mutedClientsGlobal.Add( mutePacket.clientToMute );
                                if ( mutePacket.clientToMute == clientName )
                                    clientForm.UpdateCommandWindow( "You have been muted globally by the Admin.", Color.Black, Color.IndianRed );
                            }
                            break;
                    }
                }
            }
            catch( Exception exception )
            {
                Console.WriteLine( "Client TCP Read Method Exception: " + exception.Message );
            }
        }

        private void UdpProcessServerResponse()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint( IPAddress.Any, 0 );
                while ( true )
                {
                    byte[] bytes = udpClient.Receive( ref endPoint );
                    MemoryStream memoryStream = new MemoryStream( bytes );
                    Packet packet = formatter.Deserialize( memoryStream ) as Packet;
                    switch( packet.packetType )
                    {
                        case PacketType.CHAT_MESSAGE:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                            clientForm.UpdateChatWindow( chatPacket.message, "left", Color.Black, Color.Gold );
                            break;
                        case PacketType.PRIVATE_MESSAGE:
                            PrivateMessagePacket privatePacket = (PrivateMessagePacket)packet;
                            clientForm.UpdateChatWindow( privatePacket.message, "left", Color.Black, Color.LightPink );
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

        private byte[] Encrypt( byte[] data )
        {
            lock( RSAProvider )
            {
                RSAProvider.ImportParameters( ServerKey );
                return RSAProvider.Encrypt( data, true );
            }
        }

        private byte[] Decrypt( byte[] data )
        {
            lock( RSAProvider )
            {
                RSAProvider.ImportParameters( PrivateKey );
                return RSAProvider.Decrypt( data, true );
            }
        }

        public byte[] EncryptString( string message )
        {
            return Encrypt( Encoding.UTF8.GetBytes( message ) );
        }

        public string DecryptString( byte[] message )
        {
            return Encoding.UTF8.GetString( Decrypt( message ) );
        }
    }
}