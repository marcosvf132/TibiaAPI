using System;
using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureUpdate : ServerPacket
    {
        public byte UnknownByte1 { get; set; }

        public Creature Creature { get; set; }

		public ushort CreatureType { get; set; }
		public ushort Counter { get; set; }

        public uint CreatureId { get; set; }

        public byte ManaPercent { get; set; }
        public bool Status { get; set; }
        public byte Vocation { get; set; }
        public byte Type { get; set; }
        public byte Icon { get; set; }
		
        public bool Update { get; set; }
        public bool ShowIcon { get; set; }

        public CreatureUpdate(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureUpdate;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Type = message.ReadByte();
            if (Type == (byte)CreatureUpdateType.Update) {
                CreatureType = message.ReadUInt16();
                Creature = message.ReadCreatureInstance(CreatureType);
				if (Creature == null)
					throw new Exception($"[CreatureUpdate.ParseFromNetworkMessage] Creature instance not found.");
            } else if (Type == (byte)CreatureUpdateType.Mana) {
                ManaPercent = message.ReadByte();
            } else if (Type == (byte)CreatureUpdateType.Status) {
                Status = message.ReadBool();
            } else if (Type == (byte)CreatureUpdateType.Vocation) {
                Vocation = message.ReadByte();
            } else if (Type == (byte)CreatureUpdateType.Icon) {
                ShowIcon = message.ReadBool();
				if (ShowIcon) {
					Icon = message.ReadByte();
					Update = message.ReadBool();
					Counter = message.ReadUInt16();
				}
            } else {
				throw new Exception($"[CreatureUpdate.ParseFromNetworkMessage] Unknown creature update type '{Type}'.");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureUpdate);
            message.Write(CreatureId);
            message.Write(Type);
            if (Type == (byte)CreatureUpdateType.Update) {
                message.Write(Creature, (CreatureInstanceType) CreatureType);
            } else if (Type == (byte)CreatureUpdateType.Mana) {
                message.Write(ManaPercent);
            } else if (Type == (byte)CreatureUpdateType.Status) {
                message.Write(Status);
            } else if (Type == (byte)CreatureUpdateType.Vocation) {
                message.Write(Vocation);
            } else if (Type == (byte)CreatureUpdateType.Icon) {
                message.Write(ShowIcon);
				if (ShowIcon) {
					message.Write(Icon);
					message.Write(Update);
					message.Write(Counter);
				}
            }
        }
    }
}
