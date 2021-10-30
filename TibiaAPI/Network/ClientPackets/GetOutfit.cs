using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOutfit : ClientPacket
    {
        public OutfitWindowType WindowType { get; set; }

        public ushort LookType { get; set; }

        public GetOutfit(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetOutfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
			WindowType = (OutfitWindowType)message.ReadByte();
			if (WindowType != OutfitWindowType.SelectOutfit)
				LookType = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOutfit);
			message.Write((byte)WindowType);
			if (WindowType != OutfitWindowType.SelectOutfit)
				message.Write(LookType);
        }
    }
}
