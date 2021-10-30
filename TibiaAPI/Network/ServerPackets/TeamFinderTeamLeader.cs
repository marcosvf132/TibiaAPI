using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamLeader : ServerPacket
    {
        public List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)> Members { get; } =
            new List<(uint Id, string Name, ushort Level, byte Vocation, byte Status)>();

        public uint StartTime { get; set; }

		// The next id's are part of the /assets/
        public ushort BossId { get; set; }
        public ushort HuntType { get; set; }
        public ushort HuntId { get; set; }
        public ushort QuestId { get; set; }

        public ushort FreeSlots { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort MinLevel { get; set; }
        public ushort TeamSize { get; set; }

        public byte Type { get; set; }
        public byte Vocations { get; set; } // Bit Flag

        public bool IsUpToDate { get; set; }

        public TeamFinderTeamLeader(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamLeader;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            IsUpToDate = message.ReadBool();
            if (!IsUpToDate) {
                MinLevel = message.ReadUInt16();
                MaxLevel = message.ReadUInt16();
                Vocations = message.ReadByte();
                TeamSize = message.ReadUInt16();
                FreeSlots = message.ReadUInt16();
                StartTime = message.ReadUInt32();

                Type = message.ReadByte();
				if (Type == (byte)TeamFinderType.Boss) {
					BossId = message.ReadUInt16();
				} else if (Type == (byte)TeamFinderType.Hunt) {
					HuntType = message.ReadUInt16();
					HuntId = message.ReadUInt16();
				} else if (Type == (byte)TeamFinderType.Quest) {
					QuestId = message.ReadUInt16();
				}

                Members.Capacity = message.ReadUInt16();
                for (var i = 0; i < Members.Capacity; i++) {
                    var id = message.ReadUInt32();
                    var name = message.ReadString();
                    var level = message.ReadUInt16();
                    var vocation = message.ReadByte();
                    var status = message.ReadByte();
                    Members.Add((id, name, level, vocation, status));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TeamFinderTeamLeader);
            message.Write(IsUpToDate);
            if (!IsUpToDate) {
                message.Write(MinLevel);
                message.Write(MaxLevel);
                message.Write(Vocations);
                message.Write(TeamSize);
                message.Write(FreeSlots);
                message.Write(StartTime);

                message.Write(Type);
				if (Type == (byte)TeamFinderType.Boss) {
					message.Write(BossId);
				} else if (Type == (byte)TeamFinderType.Hunt) {
					message.Write(HuntType);
					message.Write(HuntId);
				} else if (Type == (byte)TeamFinderType.Quest) {
					message.Write(QuestId);
				}

                ushort count = (ushort)Math.Min(Members.Count, ushort.MaxValue);
                message.Write(count);
                for (var i = 0; i < count; ++i) {
                    var (Id, Name, Level, Vocation, Status) = Members[i];
                    message.Write(Id);
                    message.Write(Name);
                    message.Write(Level);
                    message.Write(Vocation);
                    message.Write(Status);
                }
            }
        }
    }
}
