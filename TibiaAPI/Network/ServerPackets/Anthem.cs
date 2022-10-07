using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using System;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Anthem : ServerPacket
    {
        public byte Type { get; set; }
        public ushort MusicId { get; set; }

        public Anthem(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Anthem;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            if (Type == 0 || Type == 1 || Type == 2) {
                MusicId = message.ReadUInt16();
            } else {
                throw new Exception($"[Anthem.ParseFromNetworkMessage] Unknown type: {Type}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Anthem);
            message.Write(Type);
            if (Type == 0 || Type == 1 || Type == 2) {
                message.Write(MusicId);
            }
        }
    }
}
