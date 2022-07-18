using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class ObjectInstance : AppearanceInstance
    {
        public uint QuiverAmount { get; set; }
        public uint Charges { get; set; }
        public uint DecayTime { get; set; }
        public uint Data { get; set; }
        public uint LootCategoryFlags { get; set; }

        public bool IsLootContainer { get; set; } = false;
        public bool IsPodiumVisible { get; set; } = false;
        public bool IsQuiver { get; set; } = false;

        public byte Tier { get; set; }
        public byte PodiumDirection { get; set; }
        public byte UnknownDecayByte { get; set; }

        public OutfitInstance PodiumOutfitInstance { get; set; }
        public OutfitInstance PodiumMountInstance { get; set; }

        public ObjectInstance(uint id, Appearance type, uint data = 0) : base(id, type)
        {
            Data = data;
        }
    }
}
