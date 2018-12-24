﻿using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class AutomapFlag : ServerPacket
    {
        public Position Position { get; set; }

        public string Description { get; set; }

        public byte IconId { get; set; }

        public AutomapFlag(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaMapData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CyclopediaMapData)
            {
                return false;
            }

            Position = message.ReadPosition();
            IconId = message.ReadByte();
            Description = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaMapData);
            message.Write(Position);
            message.Write(IconId);
            message.Write(Description);
        }
    }
}
