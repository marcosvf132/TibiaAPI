using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Bosstiary : ServerPacket
    {
        public List<(uint id, byte type, uint kills, byte unknown, bool tracker)> TotalBossses { get; } = new List<(uint id, byte type, uint kills, byte unknown, bool tracker)>();
        public Bosstiary(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Bosstiary;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            TotalBossses.Capacity = message.ReadUInt16();
            for (var i = 0; i < TotalBossses.Capacity; ++i) {
                var id = message.ReadUInt32();
                var type = message.ReadByte();
                var kills = message.ReadUInt32();
                var unknown = message.ReadByte();
                var tracker = message.ReadBool();
                TotalBossses.Add((id, type, kills, unknown, tracker));
            }

        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Bosstiary);
            message.Write((ushort)TotalBossses.Capacity);
            foreach (var boss in TotalBossses) {
                message.Write(boss.id);
                message.Write(boss.type);
                message.Write(boss.kills);
                message.Write(boss.unknown);
                message.Write(boss.tracker);
            }
        }
    }
}
