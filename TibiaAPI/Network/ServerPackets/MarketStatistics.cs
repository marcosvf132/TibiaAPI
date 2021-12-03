using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketStatistics : ServerPacket
    {
        public List<(ushort ObjectId, byte Tier, ulong Price)> MarketObjects { get; } =
            new List<(ushort ObjectId, byte Tier, ulong Price)>();

        public MarketStatistics(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketStatistics;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            MarketObjects.Capacity = message.ReadUInt16();
            for (var i = 0; i < MarketObjects.Capacity; ++i) {
                var objectId = message.ReadUInt16();
                byte tier = 0;
                var obectType = Client.AppearanceStorage.GetObjectType(objectId);
                if (obectType == null)
                    throw new Exception($"[MarketStatistics.ParseFromNetworkMessage] Object type not found.");

                if (obectType.Flags.Upgradeclassification != null)
                    tier = message.ReadByte();

                var price = message.ReadUInt64();
                MarketObjects.Add((objectId, tier, price));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketStatistics);
            var count = Math.Min(MarketObjects.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (ObjectId, Tier, Price) = MarketObjects[i];
                message.Write(ObjectId);
                var obectType = Client.AppearanceStorage.GetObjectType(ObjectId);
                if (obectType == null)
                    throw new Exception($"[MarketStatistics.ParseFromNetworkMessage] Object type not found.");

                if (obectType.Flags.Upgradeclassification != null)
                    message.Write(Tier);

                message.Write(Price);
            }
        }
    }
}
