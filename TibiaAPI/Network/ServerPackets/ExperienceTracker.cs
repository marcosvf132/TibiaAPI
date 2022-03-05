using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExperienceTracker : ServerPacket
    {
        public long RawExperience { get; set; }
        public long FinalExperience { get; set; }

        public ExperienceTracker(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ExperienceTracker;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RawExperience = message.ReadInt64();
            FinalExperience = message.ReadInt64();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ExperienceTracker);
            message.Write(RawExperience);
            message.Write(FinalExperience);
        }
    }
}
