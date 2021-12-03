using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketDetail : ServerPacket
    {
        public Dictionary<MarketDetailField, string> Details { get; } = new Dictionary<MarketDetailField, string>();

        public List<(uint TotalTransactions, ulong TotalPrice, ulong MaximumPrice, ulong MinimumPrice)> BuyStatistics { get; } =
            new List<(uint TotalTransactions, ulong TotalPrice, ulong MaximumPrice, ulong MinimumPrice)>();
        public List<(uint TotalTransactions, ulong TotalPrice, ulong MaximumPrice, ulong MinimumPrice)> SellStatistics { get; } =
            new List<(uint TotalTransactions, ulong TotalPrice, ulong MaximumPrice, ulong MinimumPrice)>();

        public ushort TypeId { get; set; }

        public byte Tier { get; set; }

        public MarketDetail(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketDetail;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            TypeId = message.ReadUInt16();
            var obectType = Client.AppearanceStorage.GetObjectType(TypeId);
            if (obectType == null)
                throw new Exception($"[MarketDetail.ParseFromNetworkMessage] Object type not found.");

            if (obectType.Flags.Upgradeclassification != null)
                Tier = message.ReadByte();

            foreach (MarketDetailField value in Enum.GetValues(typeof(MarketDetailField)))
                Details.Add(value, message.ReadString());

            var timestamp = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds() * 86400;
            var tempTimestamp = timestamp;

            BuyStatistics.Capacity = message.ReadByte();
            for (var i = 0; i < BuyStatistics.Capacity; ++i) {
                tempTimestamp -= 86400;
                var totalTransactions = message.ReadUInt32();
                var totalPrice = message.ReadUInt64();
                var maximumPrice = message.ReadUInt64();
                var minimumPrice = message.ReadUInt64();
                BuyStatistics.Add((totalTransactions, totalPrice, maximumPrice, minimumPrice));
            }

            tempTimestamp = timestamp;

            SellStatistics.Capacity = message.ReadByte();
            for (var i = 0; i < SellStatistics.Capacity; ++i) {
                tempTimestamp -= 86400;
                var totalTransactions = message.ReadUInt32();
                var totalPrice = message.ReadUInt64();
                var maximumPrice = message.ReadUInt64();
                var minimumPrice = message.ReadUInt64();
                SellStatistics.Add((totalTransactions, totalPrice, maximumPrice, minimumPrice));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketDetail);
            message.Write(TypeId);
            var obectType = Client.AppearanceStorage.GetObjectType(TypeId);
            if (obectType == null)
                throw new Exception($"[MarketDetail.AppendToNetworkMessage] Object type not found.");

            if (obectType.Flags.Upgradeclassification != null)
                message.Write(Tier);

            foreach (MarketDetailField value in Enum.GetValues(typeof(MarketDetailField))) {
                if (Details.ContainsKey(value))
                    message.Write(Details[value]);
                else
                    message.Write(string.Empty);
            }

            var count = Math.Min(BuyStatistics.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < BuyStatistics.Count; ++i) {
                var (TotalTransactions, TotalPrice, MaximumPrice, MinimumPrice) = BuyStatistics[i];
                message.Write(TotalTransactions);
                message.Write(TotalPrice);
                message.Write(MaximumPrice);
                message.Write(MinimumPrice);
            }

            count = Math.Min(SellStatistics.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < SellStatistics.Count; ++i) {
                var (TotalTransactions, TotalPrice, MaximumPrice, MinimumPrice) = SellStatistics[i];
                message.Write(TotalTransactions);
                message.Write(TotalPrice);
                message.Write(MaximumPrice);
                message.Write(MinimumPrice);
            }
        }
    }
}
