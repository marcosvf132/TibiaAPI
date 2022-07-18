using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SellObject : ClientPacket
    {
        public ushort ObjectId { get; set; }
        public ushort Amount { get; set; }

        public byte Data { get; set; }

        public bool KeepEquipped { get; set; }

        public SellObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SellObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            Amount = message.ReadUInt16();
            KeepEquipped = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SellObject);
            message.Write(ObjectId);
            message.Write(Data);
            message.Write(Amount);
            message.Write(KeepEquipped);
        }
    }
}
