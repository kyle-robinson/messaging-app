using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    class Client
    {
        public IPEndPoint endPoint;
        private Socket socket;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private BinaryFormatter formatter;
        private object readLock;
        private object writeLock;
        public string name = "";
        private RSACryptoServiceProvider RSAProvider;
        public RSAParameters PublicKey;
        private RSAParameters PrivateKey;

        public Client( Socket socket )
        {
            this.socket = socket;
            
            readLock = new object();
            writeLock = new object();

            stream = new NetworkStream( socket );
            reader = new BinaryReader( stream, Encoding.UTF8 );
            writer = new BinaryWriter( stream, Encoding.UTF8 );
            formatter = new BinaryFormatter();

            RSAProvider = new RSACryptoServiceProvider( 1024 );
            PublicKey = RSAProvider.ExportParameters( false );
            PrivateKey = RSAProvider.ExportParameters( true );
        }
        
        public Packet TcpRead()
        {
            lock( readLock )
            {
                int numberOfBytes = 0;
                if ( ( numberOfBytes = reader.ReadInt32() ) != -1 )
                {
                    byte[] buffer = reader.ReadBytes( numberOfBytes );
                    MemoryStream memoryStream = new MemoryStream( buffer );
                    return formatter.Deserialize( memoryStream ) as Packet;
                }
                return null;
            }
        }

        public void TcpSend( Packet message )
        {
            lock( writeLock )
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

        private byte[] Encrypt( byte[] data )
        {
            lock( RSAProvider )
            {
                RSAProvider.ImportParameters( PublicKey );
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

        public void Close()
        {
            socket.Close();
            stream.Close();
            writer.Close();
            reader.Close();
        }
    }
}