using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseExaltationDialog : ServerPacket
    {
        public CloseExaltationDialog(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseExaltationDialog;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // Signal only
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseExaltationDialog);
            // Signal only
        }
    }
}
