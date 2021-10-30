using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaNewDetails : ServerPacket
    {
        public ushort RaceId { get; set; }

        public MonsterCyclopediaNewDetails(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaNewDetails;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaNewDetails);
            message.Write(RaceId);
        }
    }
}
