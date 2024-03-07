using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LootContainer : ClientPacket
    {
        Position Position { get; set; }

        public ContainerManagerType Type { get; set; }

        public ushort ObjectId { get; set; }

        public byte Index { get; set; }
        public byte ItemCategory { get; set; }

        public bool UseMainContainerAsFallback { get; set; }

        public LootContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LootContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = (ContainerManagerType)message.ReadByte();
            if (Type == ContainerManagerType.AddLoot) {
                ItemCategory = message.ReadByte();
                Position = message.ReadPosition();
                ObjectId = message.ReadUInt16();
                Index = message.ReadByte();
            } else if (Type == ContainerManagerType.RemoveLoot || Type == ContainerManagerType.OpenLoot) {
                ItemCategory = message.ReadByte();
            } else if (Type == ContainerManagerType.UseMainContainerAsFallback) {
                UseMainContainerAsFallback = message.ReadBool();
            } else if (Type == ContainerManagerType.AddObtain) {
                ItemCategory = message.ReadByte();
                Position = message.ReadPosition();
                ObjectId = message.ReadUInt16();
                Index = message.ReadByte();
            } else if (Type == ContainerManagerType.RemoveObtain || Type == ContainerManagerType.OpenObtain) {
                ItemCategory = message.ReadByte();
            } else {
                Client.Logger.Error($"[LootContainer.ParseFromNetworkMessage] Invalid type: {Type}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LootContainer);
            message.Write((byte)Type);
            if (Type == ContainerManagerType.AddLoot) {
                message.Write(ItemCategory);
                message.Write(Position);
                message.Write(ObjectId);
                message.Write(Index);
            } else if (Type == ContainerManagerType.RemoveLoot || Type == ContainerManagerType.OpenLoot) {
                message.Write(ItemCategory);
            } else if (Type == ContainerManagerType.AddObtain) {
                message.Write(ItemCategory);
                message.Write(Position);
                message.Write(ObjectId);
                message.Write(Index);
            } else if (Type == ContainerManagerType.RemoveObtain || Type == ContainerManagerType.OpenObtain) {
                message.Write(ItemCategory);
            } else if (Type == ContainerManagerType.UseMainContainerAsFallback) {
                message.Write(UseMainContainerAsFallback);
            }
        }
    }
}
