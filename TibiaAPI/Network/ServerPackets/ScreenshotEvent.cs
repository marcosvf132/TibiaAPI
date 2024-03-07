using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ScreenshotEvent : ServerPacket
    {
        public byte Type { get; set; }

        public ScreenshotEvent(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ScreenshotEvent;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            // 1: Achievement
            // 2: BestiaryEntryCompleted
            // 3: BestiaryEntryUnlocked
            // 4: BossDefeated
            // 5: DeathPvE
            // 6: DeathPvP
            // 7: LevelUp
            // 8: PlayerKillAssist
            // 9: PlayerKill
            // 10: PlayerAttacking
            // 11: TreasureFound
            // 12: SkillUp
            // 13: GiftOfLife
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ScreenshotEvent);
            message.Write(Type);
        }
    }
}
