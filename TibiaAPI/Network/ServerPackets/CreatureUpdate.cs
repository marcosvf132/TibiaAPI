using System;
using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureUpdate : ServerPacket
    {
        public List<(uint id, bool update, ushort counter)> Icons { get; } = new List<(uint id, bool update, ushort counter)>();

        public byte UnknownByte1 { get; set; }

        public Creature Creature { get; set; }

		public ushort CreatureType { get; set; }
		public ushort Counter { get; set; }

        public uint CreatureId { get; set; }

        public byte ManaPercent { get; set; }
        public bool Status { get; set; }
        public byte Vocation { get; set; }
        public byte Type { get; set; }
		
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
                Icons.Capacity = message.ReadByte();
                for (var i = 0; i < Icons.Capacity; ++i) {
					var id = message.ReadByte();
					var update = message.ReadBool();
					var counter = message.ReadUInt16();
                    Icons.Add((id, update, counter));
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
                message.Write((byte)Icons.Capacity);
                foreach (var icon in Icons) {
                    message.Write(icon.id);
                    message.Write(icon.update);
                    message.Write(icon.counter);
                }
            }
        }
    }
}
