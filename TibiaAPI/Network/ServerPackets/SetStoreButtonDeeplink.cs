using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetStoreButtonDeeplink : ServerPacket
    {
        public ushort UnknownUShort1 { get; set; }

        public byte StoreServiceType { get; set; }

        public SetStoreButtonDeeplink(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SetStoreButtonDeeplink;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
			UnknownUShort1 = message.ReadUInt16();
            StoreServiceType = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.SetStoreButtonDeeplink);
			message.Write(UnknownUShort1);
            message.Write(StoreServiceType);
        }
    }
}
