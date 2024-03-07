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

        public ushort DecorationAppearance { get; set; }

        public bool IsPodiumVisible { get; set; } = false;

        public byte SpecialContainer { get; set; }
        public byte Tier { get; set; }
        public byte PodiumDirection { get; set; }
        public byte IsBrandNew { get; set; }

        public string Append { get; set; } = "";

        public OutfitInstance PodiumOutfitInstance { get; set; }
        public OutfitInstance PodiumMountInstance { get; set; }

        public ObjectInstance(uint id, Appearance type, uint data = 0, string append = "") : base(id, type)
        {
            Data = data;
            Append = append;
        }
    }
}
