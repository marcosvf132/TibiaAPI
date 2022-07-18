using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuyObject : ClientPacket
    {
        public ushort ObjectId { get; set; }
        public ushort Amount { get; set; }

        public byte Data { get; set; }

        public bool IgnoreCapacity { get; set; }
        public bool WithBackpacks { get; set; }

        public BuyObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BuyObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            Amount = message.ReadUInt16();
            IgnoreCapacity = message.ReadBool();
            WithBackpacks = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuyObject);
            message.Write(ObjectId);
            message.Write(Data);
            message.Write(Amount);
            message.Write(IgnoreCapacity);
            message.Write(WithBackpacks);
        }
    }
}
