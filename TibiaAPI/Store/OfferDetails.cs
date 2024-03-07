using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Store
{
    public class OfferDetails
    {
        public List<ushort> DisabledReasons { get; } = new List<ushort>();

        public uint Id { get; set; }
        public uint Price { get; set; }

        public ushort Amount { get; set; }

        public byte HighlightState { get; set; }

        public byte IsConfirmedPrice { get; set; }
        public bool IsDisabled { get; set; }
    }
}
