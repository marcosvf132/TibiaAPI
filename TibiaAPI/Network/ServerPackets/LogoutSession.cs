using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LogoutSession : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public LogoutSession(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LogoutSession;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            UnknownByte1 = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LogoutSession);
            message.Write(UnknownByte1);
        }
    }
}
