using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketEnter : ServerPacket
    {
        public List<(ushort ObjectId, byte Tier, ushort Count)> DepotObjects { get; } =
            new List<(ushort ObjectId, byte Tier, ushort Count)>();

        public long AccountBalance { get; set; }

        public byte ActiveOffers { get; set; }

        public MarketEnter(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketEnter;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ActiveOffers = message.ReadByte();
            DepotObjects.Capacity = message.ReadUInt16();
            for (var i = 0; i < DepotObjects.Capacity; ++i) {
                var objectId = message.ReadUInt16();
				byte tier = 0;
				var obectType = Client.AppearanceStorage.GetObjectType(objectId);
				if (obectType == null)
					throw new Exception($"[MarketEnter.ParseFromNetworkMessage] Object type not found.");

				if (obectType.Flags.Upgradeclassification != null)
					tier = message.ReadByte();

                var count = message.ReadUInt16();
                DepotObjects.Add((objectId, tier, count));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketEnter);
            message.Write(ActiveOffers);
            var count = Math.Min(DepotObjects.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (ObjectId, Tier, Count) = DepotObjects[i];
                message.Write(ObjectId);
				var obectType = Client.AppearanceStorage.GetObjectType(ObjectId);
				if (obectType == null)
					throw new Exception($"[MarketEnter.AppendToNetworkMessage] Object type not found.");

				if (obectType.Flags.Upgradeclassification != null)
					message.Write(Tier);

                message.Write(Count);
            }
        }
    }
}
