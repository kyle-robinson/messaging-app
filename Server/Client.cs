using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    class Client
    {
        private Socket socket;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private BinaryFormatter formatter;
        private object readLock;
        private object writeLock;
        public string name = "";

        public Client( Socket socket )
        {
            this.socket = socket;
            
            readLock = new object();
            writeLock = new object();

            stream = new NetworkStream( socket );
            reader = new BinaryReader( stream, Encoding.UTF8 );
            writer = new BinaryWriter( stream, Encoding.UTF8 );
            formatter = new BinaryFormatter();
        }
        
        public Packet Read()
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

        public void Send( Packet message )
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

        public void Close()
        {
            socket.Close();
            stream.Close();
            writer.Close();
            reader.Close();
        }
    }
}