using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BossTracking : ServerPacket
    {
        public List<(uint id, ulong time)> BossesList { get; } = new List<(uint id, ulong time)>();
        public BossTracking(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BossTracking;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            BossesList.Capacity = message.ReadUInt16();
            for (var i = 0; i < BossesList.Capacity; ++i) {
                var id = message.ReadUInt32();
                var time = message.ReadUInt64();
                BossesList.Add((id, time));
            }

        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BossTracking);
            message.Write((ushort)BossesList.Capacity);
            foreach (var boss in BossesList) {
                message.Write(boss.id);
                message.Write(boss.time);
            }
        }
    }
}
