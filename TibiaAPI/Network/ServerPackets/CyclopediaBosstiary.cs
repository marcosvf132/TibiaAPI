using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaBosstiary : ServerPacket
    {
        public List<(uint id, byte type, uint kills, byte unknown)> TotalBossses { get; } = new List<(uint id, byte type, uint kills, byte unknown)>();
        public CyclopediaBosstiary(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaBosstiary;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            TotalBossses.Capacity = message.ReadUInt16();
            for (var i = 0; i < TotalBossses.Capacity; ++i) {
                var id = message.ReadUInt32();
                var type = message.ReadByte();
                var kills = message.ReadUInt32();
                var unknown = message.ReadByte();
                TotalBossses.Add((id, type, kills, unknown));
            }

        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaBosstiary);
            message.Write((ushort)TotalBossses.Capacity);
            foreach (var boss in TotalBossses) {
                message.Write(boss.id);
                message.Write(boss.type);
                message.Write(boss.kills);
                message.Write(boss.unknown);
            }
        }
    }
}
