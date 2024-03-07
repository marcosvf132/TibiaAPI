using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UnknownNewPacket2 : ServerPacket
    {
        public List<(ushort ItemId, byte Type, uint TimeLeft)> InventoryList { get; } =
            new List<(ushort ItemId, byte Type, uint TimeLeft)>();
        public UnknownNewPacket2(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.UnknownNewPacket2;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            InventoryList.Capacity = message.ReadUInt16();
            var cap = InventoryList.Capacity;
            for (var i = 0; i < InventoryList.Capacity; ++i) {
                var itemId = message.ReadUInt16();
                var type = message.ReadByte();
                var timeLeft = message.ReadUInt32();
                InventoryList.Add((itemId, type, timeLeft));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UnknownNewPacket2);
            var count = Math.Min(InventoryList.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (ItemId, Type, TimeLeft) = InventoryList[i];
                message.Write(ItemId);
                message.Write(Type);
                message.Write(TimeLeft);
            }

        }
    }
}
