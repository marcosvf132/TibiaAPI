using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SkillGrid : ServerPacket
    {
        public List<(byte Id, bool Locked, byte Affinity, List<byte> Mods, byte Resistance)> Gems { get; } = new List<(byte Id, bool Locked, byte Affinity, List<byte> Mods, byte Resistance)>();
        public List<ushort> BonusTable { get; } = new List<ushort>();
        public List<ushort> PromotionScrollsUsed { get; } = new List<ushort>();
        public List<byte> GemsUsed { get; } = new List<byte>();
        
        public ushort PromotionPoints { get; set; }
        public ushort PromotionScrollsPoints { get; set; }

        public uint CreatureId { get; set; }

        public bool IsInabled { get; set; }

        public byte Type { get; set; }
        public byte Vocation { get; set; }

        public SkillGrid(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SkillGrid;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            IsInabled = message.ReadBool();
            if (!IsInabled) {
                return;
            }

            Type = message.ReadByte();
            Vocation = message.ReadByte();
            PromotionPoints = message.ReadUInt16();
            PromotionScrollsPoints = message.ReadUInt16();
            BonusTable.Capacity = 36; // Change to Enum.cs
            for (var i = 1; i <= BonusTable.Capacity; ++i) {
                BonusTable.Add(message.ReadUInt16());
            }
            PromotionScrollsUsed.Capacity = message.ReadUInt16();
            for (var i = 0; i < PromotionScrollsUsed.Capacity; ++i) {
                PromotionScrollsUsed.Add(message.ReadUInt16());
            }
            GemsUsed.Capacity = message.ReadByte();
            for (var i = 0; i < GemsUsed.Capacity; ++i) {
                GemsUsed.Add(message.ReadByte());
            }
            Gems.Capacity = message.ReadByte();
            for (var i = 0; i < Gems.Capacity; ++i) {
                var id = message.ReadByte();
                var locked = message.ReadByte() > 0;
                var affinity = message.ReadByte();
                var mods = new List<byte>
                {
                    Capacity = message.ReadByte()
                };
                for (var j = 0; j < mods.Capacity; ++j) {
                    mods.Add(message.ReadByte());
                }
                var resistance = message.ReadByte();
                Gems.Add((id, locked, affinity, mods, resistance));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SkillGrid);
            message.Write(CreatureId);
            message.Write(IsInabled);
            if (!IsInabled) {
                return;
            }

            message.Write(Type);
            message.Write(Vocation);
            message.Write(PromotionPoints);
            message.Write(PromotionScrollsPoints);
            for (var i = 0; i < 35; ++i) { // Change to Enum.cs
                message.Write(BonusTable[i]);
            }
            message.Write((ushort)PromotionScrollsUsed.Capacity);
            for (var i = 0; i < PromotionScrollsUsed.Capacity; ++i) {
                message.Write(PromotionScrollsUsed[i]);
            }
            message.Write((ushort)GemsUsed.Capacity);
            for (var i = 0; i < GemsUsed.Capacity; ++i) {
                message.Write(GemsUsed[i]);
            }
            message.Write((ushort)Gems.Capacity);
            for (var i = 0; i < Gems.Capacity; ++i) {
                var (id, locked, affinity, mods, resistance) = Gems[i];
                message.Write(id);
                message.Write(locked);
                message.Write(affinity);
                message.Write((byte)mods.Capacity);
                for (var j = 0; j < mods.Capacity; ++j) {
                    message.Write(mods[j]);
                }
                message.Write(resistance);
            }
        }
    }
}
