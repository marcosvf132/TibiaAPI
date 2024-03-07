using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExaltationHistory : ServerPacket
    {
        public List<(uint Time, byte Action, string Text, byte Error)> History { get; } =
            new List<(uint Time, byte Action, string Text, byte Error)>();

        public ushort UnknownUshort1 { get; set; }
        public ushort UnknownUshort2 { get; set; }
        public ExaltationHistory(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ExaltationHistory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            UnknownUshort1 = message.ReadUInt16();
            UnknownUshort2 = message.ReadUInt16();
            History.Capacity = message.ReadByte();
            for (var i = 0; i < History.Capacity; ++i) {
                var time = message.ReadUInt32();
                var action = message.ReadByte();
                var text = message.ReadString();
                var error = message.ReadByte();
                History.Add((time, action, text, error));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ExaltationHistory);
            message.Write(UnknownUshort1);
            message.Write(UnknownUshort2);
            var count = Math.Min(History.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i) {
                var (time, action, text, error) = History[i];
                message.Write(time);
                message.Write(action);
                message.Write(text);
                message.Write(error);
            }
        }
    }
}
