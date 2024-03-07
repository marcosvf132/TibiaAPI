using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ImbuementDurations : ClientPacket
    {
        public byte Type { get; set; }

        public ImbuementDurations(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ImbuementDurations;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ImbuementDurations);
            message.Write(Type);
        }
    }
}
