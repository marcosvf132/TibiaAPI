using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PassiveAbilityData : ServerPacket
    {
        public uint UnknownUint1 { get; set; }
        public uint UnknownUint2 { get; set; } 

        public byte UnknownByte1 { get; set; }
        public byte UnknownByte2 { get; set; }
        public byte UnknownByte3 { get; set; }
        public byte UnknownByte4 { get; set; }
        
        public byte UnknownType1 { get; set; }

        public PassiveAbilityData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PassiveAbilityData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            UnknownByte1 = message.ReadByte();
            UnknownType1 = message.ReadByte();
            if (UnknownType1 == 0) {
                UnknownUint1 = message.ReadUInt32();
                UnknownUint2 = message.ReadUInt32();
                UnknownByte2 = message.ReadByte();
            } else if (UnknownType1 == 1) {
                UnknownByte3 = message.ReadByte();
                UnknownByte4 = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PassiveAbilityData);
            message.Write(UnknownByte1);
            message.Write(UnknownType1);
            if (UnknownType1 == 0) {
                message.Write(UnknownUint1);
                message.Write(UnknownUint2);
                message.Write(UnknownByte2);
            } else if (UnknownType1 == 1) {
                message.Write(UnknownByte3);
                message.Write(UnknownByte4);
            }
        }
    }
}
