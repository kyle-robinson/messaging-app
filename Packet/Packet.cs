using System;
using System.Net;
using System.Security.Cryptography;

public enum PacketType
{
    EMPTY,
    CHAT_MESSAGE,
    PRIVATE_MESSAGE,
    ENCRYPTED_MESSAGE,
    NICKNAME,
    CLIENT_LIST,
    LOGIN,
    PUBLIC_KEY
}

[Serializable]
public class Packet
{
    public PacketType packetType { get; set; }
}

[Serializable]
public class ChatMessagePacket : Packet
{
    public string message;
    public ChatMessagePacket( string message )
    {
        this.message = message;
        packetType = PacketType.CHAT_MESSAGE;
    }
}

[Serializable]
public class PrivateMessagePacket : Packet
{
    public string message;
    public string name;
    public PrivateMessagePacket( string message, string name )
    {
        this.message = message;
        this.name = name;
        packetType = PacketType.PRIVATE_MESSAGE;
    }
}

[Serializable]
public class EncryptedMessagePacket : Packet
{
    public byte[] message;
    public EncryptedMessagePacket( byte[] message )
    {
        this.message = message;
        packetType = PacketType.ENCRYPTED_MESSAGE;
    }
}

[Serializable]
public class NicknamePacket : Packet
{
    public string name;
    public NicknamePacket( string name )
    {
        this.name = name;
        packetType = PacketType.NICKNAME;
    }
}

[Serializable]
public class ClientListPacket : Packet
{
    public string name;
    public bool removeText;
    public ClientListPacket( string name, bool removeText )
    {
        this.name = name;
        this.removeText = removeText;
        packetType = PacketType.CLIENT_LIST;
    }
}

[Serializable]
public class LoginPacket : Packet
{
    public IPEndPoint EndPoint;
    public RSAParameters PublicKey;
    public LoginPacket( IPEndPoint EndPoint, RSAParameters PublicKey )
    {
        this.EndPoint = EndPoint;
        this.PublicKey = PublicKey;
        packetType = PacketType.LOGIN;
    }
}