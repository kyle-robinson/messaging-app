using System;

public enum PacketType
{
    EMPTY,
    SERVER_MESSAGE,
    CHAT_MESSAGE,
    PRIVATE_MESSAGE,
    NICKNAME,
    CLIENT_LIST
}

[Serializable]
public class Packet
{
    public PacketType packetType { get; set; }
}

[Serializable]
public class EmptyPacket : Packet
{
    public EmptyPacket()
    {
        packetType = PacketType.EMPTY;
    }
}

[Serializable]
public class ServerMessagePacket : Packet
{
    public string message;
    public ServerMessagePacket( string message )
    {
        this.message = message;
        packetType = PacketType.SERVER_MESSAGE;
    }
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