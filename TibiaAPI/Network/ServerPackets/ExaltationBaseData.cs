using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExaltationBaseData : ServerPacket
    {
        public List<(byte Id, List<(byte Target, ulong Price)> Tiers)> Classifications { get; } =
            new List<(byte Id, List<(byte Target, ulong Price)> Tiers)>();

        public List<(byte Tier, byte Value)> ExaltedCorePerTier { get; } = new List<(byte Tier, byte Value)>();
 
		public ushort PlayerMaxDust { get; set; }
		public ushort MaxDustCap { get; set; }

		public byte DustPercent { get; set; }
		public byte DustToSleaver { get; set; }
		public byte SliverToCore { get; set; }
		public byte DustPercentUpgrade { get; set; }
		public byte DustFusion { get; set; }
		public byte DustTransfer { get; set; }
		public byte ChanceBase { get; set; }
		public byte ChanceImproved { get; set; }
		public byte ReduceTierLoss { get; set; }

        public ExaltationBaseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ExaltationBaseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
			Classifications.Capacity = message.ReadByte();
			for (var i = 0; i < Classifications.Capacity; i++) {
				var id = message.ReadByte();
                var tiers = new List<(byte Target, ulong Price)>
                {
                    Capacity = message.ReadByte()
                };
                for (var k = 0; k < tiers.Capacity; k++) {
					var target = message.ReadByte();
					var price = message.ReadUInt64();
					tiers.Add((target, price));
				}
				Classifications.Add((id, tiers));
			}

			ExaltedCorePerTier.Capacity = message.ReadByte();
			for (var i = 0; i < ExaltedCorePerTier.Capacity; i++) {
				ExaltedCorePerTier.Add((message.ReadByte(), message.ReadByte()));
			}

			// Fix for 13.30 iterations
			message.ReadByte();
			message.ReadByte();
			DustPercent = message.ReadByte();
			DustToSleaver = message.ReadByte();
			SliverToCore = message.ReadByte();
			DustPercentUpgrade = message.ReadByte();
			PlayerMaxDust = message.ReadUInt16();
			MaxDustCap = message.ReadUInt16();
			DustFusion = message.ReadByte();
			DustTransfer = message.ReadByte();
			ChanceBase = message.ReadByte();
			ChanceImproved = message.ReadByte();
			ReduceTierLoss = message.ReadByte();

			message.ReadByte();
			message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ExaltationBaseData);

			byte count = (byte)Math.Min(Classifications.Count, byte.MaxValue);
			message.Write(count);
			for (var i = 0; i < count; ++i) {
				var (Id, Tiers) = Classifications[i];
				message.Write(Id);
				byte tiers = (byte)Math.Min(Tiers.Count, byte.MaxValue);
				message.Write(tiers);
				for (var k = 0; k < tiers; ++k) {
					var (Target, Price) = Tiers[k];
					message.Write(Target);
					message.Write(Price);
				}
			}

			count = (byte)Math.Min(ExaltedCorePerTier.Count, byte.MaxValue);
			message.Write(count);
			for (var i = 0; i < count; ++i) {
				var tierCore = ExaltedCorePerTier[i];
				message.Write(tierCore.Tier);
				message.Write(tierCore.Value);
			}
			message.Write(DustPercent);
			message.Write(DustToSleaver);
			message.Write(SliverToCore);
			message.Write(DustPercentUpgrade);
			message.Write(PlayerMaxDust);
			message.Write(MaxDustCap);
			message.Write(DustFusion);
			message.Write(DustTransfer);
			message.Write(ChanceBase);
			message.Write(ChanceImproved);
			message.Write(ReduceTierLoss);
        }
    }
}
