using System;
using System.Collections.Generic;
using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ConfigureBossPodium : ServerPacket
    {
        public bool IsPodiumVisible { get; set; }
        public bool IsBossVisible { get; set; }
        public byte StackPos { get; set; }
        public byte Direction { get; set; }
        public ushort UnknownUshort { get; set; }
        public ushort ItemId { get; set; }
        public Position Position { get; set; }
        public OutfitInstance SelectedBossOutfit { get; set; }
        public List<(string Name, uint Id, OutfitInstance Outfit)> UnlockedBosses { get; } = new List<(string Name, uint Id, OutfitInstance Outfit)>();
        public ConfigureBossPodium(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ConfigureBossPodium;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            SelectedBossOutfit = (OutfitInstance)message.ReadCreatureOutfit();
            UnknownUshort = message.ReadUInt16(); // Maybe its mount data? But why nothing show ingame?
            UnlockedBosses.Capacity = message.ReadUInt16();
            for (var i = 0; i < UnlockedBosses.Capacity; ++i) {
                var name = message.ReadString();
                var id = message.ReadUInt32();
                var outfit = (OutfitInstance)message.ReadCreatureOutfit();
                UnlockedBosses.Add((name, id, outfit));
            }

            Position = message.ReadPosition();
            ItemId = message.ReadUInt16();
            StackPos = message.ReadByte();
            IsPodiumVisible = message.ReadBool();
            IsBossVisible = message.ReadBool();
            Direction = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ConfigureBossPodium);
            message.Write(SelectedBossOutfit);
            message.Write(UnknownUshort);
            message.Write((ushort)UnlockedBosses.Capacity);
            foreach (var boss in UnlockedBosses) {
                message.Write(boss.Name);
                message.Write(boss.Id);
                message.Write(boss.Outfit);
            }

            message.Write(Position);
            message.Write(ItemId);
            message.Write(StackPos);
            message.Write(IsPodiumVisible);
            message.Write(IsBossVisible);
            message.Write(Direction);
        }
    }
}
