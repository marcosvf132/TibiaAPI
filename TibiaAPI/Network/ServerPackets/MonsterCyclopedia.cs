using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopedia : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)> Charms { get; } =
            new List<(byte Id, string Name, string Description, byte Type, ushort CharmPoints, bool IsPurchased, bool IsAssigned, ushort RaceId, uint RemovalCost)>();
        public List<ushort> CharmAssignableRaceIds { get; } = new List<ushort>();
        public List<(string Name, ushort Total, ushort Known)> RaceCollections { get; } =
            new List<(string Name, ushort Total, ushort Known)>();

        public uint CharmPoints { get; set; }

        public byte UnassignedCharms { get; set; }

        public MonsterCyclopedia(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopedia;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceCollections.Capacity = message.ReadUInt16();
            for (var i = 0; i < RaceCollections.Capacity; ++i) {
                var name = message.ReadString();
                var total = message.ReadUInt16();
                var known = message.ReadUInt16();
                RaceCollections.Add((name, total, known));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopedia);

            var count = Math.Min(RaceCollections.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (Name, Total, Known) = RaceCollections[i];
                message.Write(Name);
                message.Write(Total);
                message.Write(Known);
            }
        }
    }
}
