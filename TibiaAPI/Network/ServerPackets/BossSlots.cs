using OXGaming.TibiaAPI.Constants;
using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BossSlots : ServerPacket
    {
        public bool IsAnyBossUnlocked { get; set; }

        public ushort EquipmentLootBonus { get; set; }
        public ushort NextEquipmentLootBonus { get; set; }

        public uint Points { get; set; }
        public uint TotalPoints { get; set; }

        public List<(uint id, byte type)> UnlockedBosses { get; } = new List<(uint id, byte type)>();

        #region First slot
        public bool IsFirstSlotUnlocked { get; set; }
        public bool FirstSlotIsInactive { get; set; }

        public byte FirstSlotRaceType { get; set; }
        public byte FirstSlotKillBonus { get; set; }
        public byte FirstSlotUnknownEnum { get; set; }

        public uint FirstSlotUnlockAmountOrSelectedBossId { get; set; }
        public uint FirstSlotSlainAmount { get; set; }
        public uint FirstSlotPriceToRemove { get; set; }

        public ushort FirstSlotEquipmentLootBonus { get; set; }
        #endregion

        #region Second slot
        public bool IsSecondSlotUnlocked { get; set; }
        public bool SecondSlotIsInactive { get; set; }

        public byte SecondSlotRaceType { get; set; }
        public byte SecondSlotKillBonus { get; set; }
        public byte SecondSlotUnknownEnum { get; set; }

        public uint SecondSlotUnlockAmountOrSelectedBossId { get; set; }
        public uint SecondSlotSlainAmount { get; set; }
        public uint SecondSlotPriceToRemove { get; set; }

        public ushort SecondSlotEquipmentLootBonus { get; set; }
        #endregion

        #region Today boss slot
        public bool IsTodaySlotUnlocked { get; set; }
        public bool TodaySlotIsInactive { get; set; }

        public byte TodaySlotRaceType { get; set; }
        public byte TodaySlotKillBonus { get; set; }
        public byte TodaySlotUnknownEnum { get; set; }

        public uint TodaySlotUnlockAmountOrSelectedBossId { get; set; }
        public uint TodaySlotSlainAmount { get; set; }
        public uint TodaySlotPriceToRemove { get; set; }

        public ushort TodaySlotEquipmentLootBonus { get; set; }
        #endregion


        public BossSlots(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BossSlots;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Points = message.ReadUInt32();
            TotalPoints = message.ReadUInt32();
            EquipmentLootBonus = message.ReadUInt16();
            NextEquipmentLootBonus = message.ReadUInt16();

            IsFirstSlotUnlocked = message.ReadBool();
            FirstSlotUnlockAmountOrSelectedBossId = message.ReadUInt32();
            if (IsFirstSlotUnlocked && FirstSlotUnlockAmountOrSelectedBossId > 0) {
                FirstSlotRaceType = message.ReadByte();
                FirstSlotSlainAmount = message.ReadUInt32();
                FirstSlotEquipmentLootBonus = message.ReadUInt16();
                FirstSlotKillBonus = message.ReadByte();
                FirstSlotUnknownEnum = message.ReadByte();
                FirstSlotPriceToRemove = message.ReadUInt32();
                FirstSlotIsInactive = message.ReadBool();
            }
            
            IsSecondSlotUnlocked = message.ReadBool();
            SecondSlotUnlockAmountOrSelectedBossId = message.ReadUInt32();
            if (IsSecondSlotUnlocked && SecondSlotUnlockAmountOrSelectedBossId > 0) {
                SecondSlotRaceType = message.ReadByte();
                SecondSlotSlainAmount = message.ReadUInt32();
                SecondSlotEquipmentLootBonus = message.ReadUInt16();
                SecondSlotKillBonus = message.ReadByte();
                SecondSlotUnknownEnum = message.ReadByte();
                SecondSlotPriceToRemove = message.ReadUInt32();
                SecondSlotIsInactive = message.ReadBool();
            }
            
            IsTodaySlotUnlocked = message.ReadBool();
            TodaySlotUnlockAmountOrSelectedBossId = message.ReadUInt32();
            if (IsTodaySlotUnlocked && TodaySlotUnlockAmountOrSelectedBossId > 0) {
                TodaySlotRaceType = message.ReadByte();
                TodaySlotSlainAmount = message.ReadUInt32();
                TodaySlotEquipmentLootBonus = message.ReadUInt16();
                TodaySlotKillBonus = message.ReadByte();
                TodaySlotUnknownEnum = message.ReadByte();
                TodaySlotPriceToRemove = message.ReadUInt32();
                TodaySlotIsInactive = message.ReadBool();
            }

            IsAnyBossUnlocked = message.ReadBool();
            if (IsAnyBossUnlocked) {
                UnlockedBosses.Capacity = message.ReadUInt16();
                for (var i = 0; i < UnlockedBosses.Capacity; ++i) {
                    var id = message.ReadUInt32();
                    var type = message.ReadByte();
                    UnlockedBosses.Add((id, type));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BossSlots);
            message.Write(Points);
            message.Write(TotalPoints);
            message.Write(EquipmentLootBonus);
            message.Write(NextEquipmentLootBonus);

            message.Write(IsFirstSlotUnlocked);
            message.Write(FirstSlotUnlockAmountOrSelectedBossId);
            if (IsFirstSlotUnlocked && FirstSlotUnlockAmountOrSelectedBossId > 0) {
                message.Write(FirstSlotRaceType);
                message.Write(FirstSlotSlainAmount);
                message.Write(FirstSlotEquipmentLootBonus);
                message.Write(FirstSlotKillBonus);
                message.Write(FirstSlotUnknownEnum);
                message.Write(FirstSlotPriceToRemove);
                message.Write(FirstSlotIsInactive);
            }

            message.Write(IsSecondSlotUnlocked);
            message.Write(SecondSlotUnlockAmountOrSelectedBossId);
            if (IsSecondSlotUnlocked && SecondSlotUnlockAmountOrSelectedBossId > 0) {
                message.Write(SecondSlotRaceType);
                message.Write(SecondSlotSlainAmount);
                message.Write(SecondSlotEquipmentLootBonus);
                message.Write(SecondSlotKillBonus);
                message.Write(SecondSlotUnknownEnum);
                message.Write(SecondSlotPriceToRemove);
                message.Write(SecondSlotIsInactive);
            }

            message.Write(IsTodaySlotUnlocked);
            message.Write(TodaySlotUnlockAmountOrSelectedBossId);
            if (IsTodaySlotUnlocked && TodaySlotUnlockAmountOrSelectedBossId > 0) {
                message.Write(TodaySlotRaceType);
                message.Write(TodaySlotSlainAmount);
                message.Write(TodaySlotEquipmentLootBonus);
                message.Write(TodaySlotKillBonus);
                message.Write(TodaySlotUnknownEnum);
                message.Write(TodaySlotPriceToRemove);
                message.Write(TodaySlotIsInactive);
            }

            message.Write(IsAnyBossUnlocked);
            if (IsAnyBossUnlocked) {
                message.Write((ushort)UnlockedBosses.Capacity);
                foreach (var unlockedBoss in UnlockedBosses) {
                    message.Write(unlockedBoss.id);
                    message.Write(unlockedBoss.type);
                }
            }

        }
    }
}
