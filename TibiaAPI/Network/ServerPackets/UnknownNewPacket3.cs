using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UnknownNewPacket3 : ServerPacket
    {
        public UnknownNewPacket3(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.UnknownNewPacket3;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Client.Logger.Error($"UnknownNewPacket3: EMPTY");
            //
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UnknownNewPacket3);
            //
        }
    }
}
