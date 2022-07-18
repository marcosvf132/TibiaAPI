using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketBrowse : ClientPacket
    {
        public byte Type { get; set; }
        public ushort ObjectId { get; set; }

        public MarketBrowse(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketBrowse;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            // Type '1' means 'own offers browse'
            // Type '2' means 'own history browse'
            // Type '3' means 'item browse'
            if (Type == 3) {
                ObjectId = message.ReadUInt16();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketBrowse);
            message.Write(Type);
            if (Type == 3) {
                message.Write(ObjectId);
            }
        }
    }
}
