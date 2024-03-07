using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BugReportAllowed : ServerPacket
    {
        public bool Enabled { get; set; }

        public BugReportAllowed(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BugReportAllowed;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Enabled = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BugReportAllowed);
            message.Write(Enabled);
        }
    }
}
