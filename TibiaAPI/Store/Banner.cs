namespace OXGaming.TibiaAPI.Store
{
    public class Banner
    {
        public string Category { get; set; }
        public string Collection { get; set; }
        public string Image { get; set; }

        public uint OfferId { get; set; }

        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }

        public byte Type { get; set; }
    }
}
