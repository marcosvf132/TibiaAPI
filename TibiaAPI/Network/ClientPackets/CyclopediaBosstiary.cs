using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Bosstiary : ClientPacket
    {

        public Bosstiary(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Bosstiary;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Bosstiary);
        }
    }
}
