using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerState : ServerPacket
    {
        public byte IconCounter { get; set; }

        public uint State { get; set; }

        public PlayerState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerState;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            State = message.ReadUInt32();
            IconCounter = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerState);
            message.Write(State);
            message.Write(IconCounter);
        }
    }
}
