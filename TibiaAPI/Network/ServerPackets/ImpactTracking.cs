using OXGaming.TibiaAPI.Constants;
using System;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ImpactTracking : ServerPacket
    {
        public uint Amount { get; set; }

        public string Target { get; set; }

        public bool IsDamage { get; set; }

        public ElementType Element { get; set; }

        public byte Type { get; set; }

        public ImpactTracking(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ImpactTracking;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            if (Type == (byte)ImpactAnalyzer.Heal) {
                Amount = message.ReadUInt32();
            } else if (Type == (byte)ImpactAnalyzer.DamageDealt) {
                Amount = message.ReadUInt32();
                Element = (ElementType)message.ReadByte();
            } else if (Type == (byte)ImpactAnalyzer.DamageReceived) {
                Amount = message.ReadUInt32();
                Element = (ElementType)message.ReadByte();
                Target = message.ReadString();
            } else {
                throw new Exception($"[ImpactTracking.ParseFromNetworkMessage] Unknown impact type {Type}.");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImpactTracking);
            message.Write(Type);
            if (Type == (byte)ImpactAnalyzer.Heal) {
                message.Write(Amount);
            } else if (Type == (byte)ImpactAnalyzer.DamageDealt) {
                Amount = message.ReadUInt32();
                message.Write((byte)Element);
            } else if (Type == (byte)ImpactAnalyzer.DamageReceived) {
                message.Write(Amount);
                message.Write((byte)Element);
                message.Write(Target);
            }
        }
    }
}
