using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SkillGrid : ClientPacket
    {

        public uint OwnerId { get; set; }

        public SkillGrid(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SkillGrid;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OwnerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SkillGrid);
            message.Write(OwnerId);
        }
    }
}
