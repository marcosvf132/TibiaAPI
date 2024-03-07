using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenForge : ClientPacket
    {
        public byte[] UnknownBytes { get; set; }

        public OpenForge(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenForge;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // 00 00 09
            UnknownBytes = message.ReadBytes(3);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ClientPacketType.OpenForge);
            message.Write(UnknownBytes);
        }
    }
}
