using OXGaming.TibiaAPI.Constants;
using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TeamFinderTeamMember : ServerPacket
    {
        public List<(uint LeaderId, string LeaderName, ushort MinLevel, ushort MaxLevel, byte Vocation, ushort Slots, ushort MissingSlots, uint TimeStamp, byte Type, ushort BossId, ushort HuntType, ushort HuntId, ushort QuestId, byte Status)> TeamFinder { get; } =
            new List<(uint LeaderId, string LeaderName, ushort MinLevel, ushort MaxLevel, byte Vocation, ushort Slots, ushort MissingSlots, uint TimeStamp, byte Type, ushort BossId, ushort HuntType, ushort HuntId, ushort QuestId, byte Status)>();

		public bool IsUpToDate { get; set; }

        public TeamFinderTeamMember(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TeamFinderTeamMember;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
			IsUpToDate = message.ReadBool();
			if (!IsUpToDate) {
                TeamFinder.Capacity = message.ReadUInt16();
                for (var i = 0; i < TeamFinder.Capacity; i++) {
					var leaderId = message.ReadUInt32();
					var leaderName = message.ReadString();
					var minLevel = message.ReadUInt16();
					var maxLevel = message.ReadUInt16();
					var vocation = message.ReadByte();
					var slots = message.ReadUInt16();
					var missingSlots = message.ReadUInt16();
					var timeStamp = message.ReadUInt32();
					var type = message.ReadByte();

					ushort bossId = 0;
					ushort huntType = 0;
					ushort huntId = 0;
					ushort questId = 0;
					if (type == (byte)TeamFinderType.Boss) {
						bossId = message.ReadUInt16();
					} else if (type == (byte)TeamFinderType.Hunt) {
						huntType = message.ReadUInt16();
						huntId = message.ReadUInt16();
					} else if (type == (byte)TeamFinderType.Quest) {
						questId = message.ReadUInt16();
					}

					var status = message.ReadByte();
					TeamFinder.Add((leaderId, leaderName, minLevel, maxLevel, vocation, slots, missingSlots, timeStamp, type, bossId, huntType, huntId, questId, status));
				}
			}
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TeamFinderTeamMember);
			message.Write(IsUpToDate);
			if (!IsUpToDate) {
                ushort count = (ushort)System.Math.Min(TeamFinder.Count, ushort.MaxValue);
                message.Write(count);
                for (var i = 0; i < count; i++) {
                    var (leaderId, leaderName, minLevel, maxLevel, vocation, slots, missingSlots, timeStamp, type, bossId, huntType, huntId, questId, status) = TeamFinder[i];
					message.Write(leaderId);
					message.Write(leaderName);
					message.Write(minLevel);
					message.Write(maxLevel);
					message.Write(vocation);
					message.Write(slots);
					message.Write(missingSlots);
					message.Write(timeStamp);
					message.Write(type);
					if (type == (byte)TeamFinderType.Boss) {
						message.Write(bossId);
					} else if (type == (byte)TeamFinderType.Hunt) {
						message.Write(huntType);
						message.Write(huntId);
					} else if (type == (byte)TeamFinderType.Quest) {
						message.Write(questId);
					}

					message.Write(status);
				}
			}
        }
    }
}
