using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{ 
    public class ImbuementDurations : ServerPacket
    {
        public List<(byte SlotID, Appearances.ObjectInstance Item, List<(bool HasImbue, string ImbueName, ushort IconID, uint Duration, bool? IsTimeCounting)> Slots)> InventoryList { get; } =
            new List<(byte SlotID, Appearances.ObjectInstance Item, List<(bool HasImbue, string ImbueName, ushort IconID, uint Duration, bool? IsTimeCounting)> Slots)>();
        public ImbuementDurations(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ImbuementDurations;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            InventoryList.Capacity = message.ReadByte();
            for (var i = 0; i < InventoryList.Capacity; ++i) {
                var slotID = message.ReadByte();
                var item = message.ReadObjectInstance();
                var slots = new List<(bool HasImbue, string ImbueName, ushort IconID, uint Duration, bool? IsTimeCounting)>(message.ReadByte());
                for (var j = 0; j < slots.Capacity; ++j) {
                    bool hasImbue = message.ReadBool();
                    var imbueName = string.Empty;
                    ushort iconID = 0;
                    uint duration = 0;
                    bool? isTimeCounting = null;
                    if (hasImbue) {
                        imbueName = message.ReadString();
                        iconID = message.ReadUInt16();
                        duration = message.ReadUInt32();
                        isTimeCounting = message.ReadBool();
                    }
                    slots.Add((hasImbue, imbueName, iconID, duration, isTimeCounting));
                }
                InventoryList.Add((slotID, item, slots));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImbuementDurations);
            var count = Math.Min(InventoryList.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i) {
                var (slotID, item, slots) = InventoryList[i];
                message.Write(slotID);
                message.Write(item);
                var countInternal = Math.Min(slots.Count, byte.MaxValue);
                message.Write((byte)countInternal);
                for (var j = 0; j < countInternal; ++j) {
                    var (hasImbue, imbueName, iconID, duration, isTimeCounting) = slots[j];
                    message.Write(hasImbue);
                    if (hasImbue) {
                        message.Write(imbueName);
                        message.Write(iconID);
                        message.Write(duration);
                        message.Write((bool)isTimeCounting);
                    }
                }
            }
        }
    }
}
