using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ForgingBasicData : ServerPacket
    {
        public List<(byte Id, List<(byte Target, ulong Price)> Tiers)> Classifications { get; } =
            new List<(byte Id, List<(byte Target, ulong Price)> Tiers)>();

		public byte ConversionIncreaseDustLimitInput { get; set; }
		public byte ConversionConvertDustOutput { get; set; }
		public byte ConversionConvertSilversInput { get; set; }
		public byte UnknownByte1 { get; set; }
		public byte ConversionIncreaseDustLimitOutput { get; set; }
		public byte UnknownByte2 { get; set; }
		public byte FusionDustInput { get; set; }
		public byte TransferDustInput { get; set; }
		public byte FusionSuccessRatePercent { get; set; }
		public byte FusionSuccessRateBonus { get; set; }
		public byte FusionTierLossBonus { get; set; }

        public ForgingBasicData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ForgingBasicData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
			Classifications.Capacity = message.ReadByte();
			for (var i = 0; i < Classifications.Capacity; i++) {
				var id = message.ReadByte();
				var tiers = new List<(byte Target, ulong Price)>();
				tiers.Capacity = message.ReadByte();
				for (var k = 0; k < tiers.Capacity; k++) {
					var target = message.ReadByte();
					var price = message.ReadUInt64();
					tiers.Add((target, price));
				}
				Classifications.Add((id, tiers));
			}

			ConversionIncreaseDustLimitInput = message.ReadByte();
			ConversionConvertDustOutput = message.ReadByte();
			ConversionConvertSilversInput = message.ReadByte();
			UnknownByte1 = message.ReadByte();
			ConversionIncreaseDustLimitOutput = message.ReadByte();
			UnknownByte2 = message.ReadByte();
			FusionDustInput = message.ReadByte();
			TransferDustInput = message.ReadByte();
			FusionSuccessRatePercent = message.ReadByte();
			FusionSuccessRateBonus = message.ReadByte();
			FusionTierLossBonus = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ForgingBasicData);

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

			message.Write(ConversionIncreaseDustLimitInput);
			message.Write(ConversionConvertDustOutput);
			message.Write(ConversionConvertSilversInput);
			message.Write(UnknownByte1);
			message.Write(ConversionIncreaseDustLimitOutput);
			message.Write(UnknownByte2);
			message.Write(FusionDustInput);
			message.Write(TransferDustInput);
			message.Write(FusionSuccessRatePercent);
			message.Write(FusionSuccessRateBonus);
			message.Write(FusionTierLossBonus);
        }
    }
}
