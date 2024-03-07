using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PurchaseSorting : ClientPacket
    {
        public bool Toggle { get; set; }

        public PurchaseSorting(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PurchaseSorting;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Toggle = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PurchaseSorting);
            message.Write(Toggle);
        }
    }
}
