using System;
using System.Net;
using System.Security.Cryptography;

public enum PacketType
{
    EMPTY,
    LOGIN,
    ENCRYPTED_ADMIN,
    ENCRYPTED_SERVER,
    CHAT_MESSAGE,
    ENCRYPTED_MESSAGE,
    PRIVATE_MESSAGE,
    ENCRYPTED_PRIVATE_MESSAGE,
    ENCRYPTED_NICKNAME,
    ENCRYPTED_CLIENT_LIST,
    GLOBAL_MUTE,
    ENCRYPTED_GAME
}

[Serializable]
public class Packet
{
    public PacketType packetType { get; set; }
}

/*   ADMINISTRATION   */
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

[Serializable]
public class EncryptedAdminPacket : Packet
{
    public byte[] adminConnected;
    public EncryptedAdminPacket( byte[] adminConnected )
    {
        this.adminConnected = adminConnected;
        packetType = PacketType.ENCRYPTED_ADMIN;
    }
}

[Serializable]
public class EncryptedServerPacket : Packet
{
    public byte[] message;
    public EncryptedServerPacket( byte[] message )
    {
        this.message = message;
        packetType = PacketType.ENCRYPTED_SERVER;
    }
}

/*   MESSAGES   */
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
public class EncryptedPrivateMessagePacket : Packet
{
    public byte[] message;
    public byte[] name;
    public EncryptedPrivateMessagePacket( byte[] message, byte[] name )
    {
        this.message = message;
        this.name = name;
        packetType = PacketType.ENCRYPTED_PRIVATE_MESSAGE;
    }
}

/*   NAMING/CLIENTS   */
[Serializable]
public class EncryptedNicknamePacket : Packet
{
    public byte[] name;
    public EncryptedNicknamePacket( byte[] name )
    {
        this.name = name;
        packetType = PacketType.ENCRYPTED_NICKNAME;
    }
}

[Serializable]
public class EncryptedClientListPacket : Packet
{
    public byte[] name;
    public byte[] removeText;
    public EncryptedClientListPacket( byte[] name, byte[] removeText )
    {
        this.name = name;
        this.removeText = removeText;
        packetType = PacketType.ENCRYPTED_CLIENT_LIST;
    }
}

/*   MISCELLANEOUS   */
[Serializable]
public class GlobalMutePacket : Packet
{
    public string clientToMute;
    public GlobalMutePacket( string clientToMute )
    {
        this.clientToMute = clientToMute;
        packetType = PacketType.GLOBAL_MUTE;
    }
}

[Serializable]
public class EncryptedGamePacket : Packet
{
    public byte[] userGuess;
    public EncryptedGamePacket( byte[] userGuess )
    {
        this.userGuess = userGuess;
        packetType = PacketType.ENCRYPTED_GAME;
    }
}