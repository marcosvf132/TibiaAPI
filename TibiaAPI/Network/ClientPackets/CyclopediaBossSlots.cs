using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaBossSlots : ClientPacket
    {

        public CyclopediaBossSlots(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CyclopediaBossSlots;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CyclopediaBossSlots);
        }
    }
}
