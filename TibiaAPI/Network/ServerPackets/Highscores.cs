using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Highscores : ServerPacket
    {
        public List<(byte Id, string Category)> Categories { get; } = new List<(byte Id, string Category)>();
        public List<(uint Rank, string Character, string UnknownString, byte VocationId, string World, ushort Level, bool IsCurrentPlayer, ulong Points)> Entries { get; } =
            new List<(uint Rank, string Character, string UnknownString, byte VocationId, string World, ushort Level, bool IsCurrentPlayer, ulong Points)>();
        public List<string> GameWorlds { get; } = new List<string>();
        public List<(uint Id, string Vocation)> VocationOptions { get; } = new List<(uint Id, string Text)>();

        public string SelectedWorld { get; set; }

        public uint LastUpdateTimestamp { get; set; }
        public uint SelectedVocation { get; set; }

        public ushort CurrentPage { get; set; }
        public ushort NumberOfPages { get; set; }
		
        public byte SelectedCategory { get; set; }
        public byte WorldCategory { get; set; }
        public byte UnknownByte1 { get; set; }
        public byte UnknownByte2 { get; set; }
        public byte UnknownByte3 { get; set; }

        public bool NoData { get; set; }
        public bool BattleEye { get; set; }
		
        public Highscores(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            NoData = message.ReadBool();
			if (NoData)
				return;
			
            GameWorlds.Capacity = message.ReadByte();
            for (var i = 0; i < GameWorlds.Capacity; i++)
                GameWorlds.Add(message.ReadString());
			
            SelectedWorld = message.ReadString();
			
			WorldCategory = message.ReadByte();
			BattleEye = message.ReadBool();
			
            VocationOptions.Capacity = message.ReadByte();
            for (var i = 0; i < VocationOptions.Capacity; i++) {
                var id = message.ReadUInt32();
                var vocation = message.ReadString();
                VocationOptions.Add((id, vocation));
            }
            SelectedVocation = message.ReadUInt32();
			
            Categories.Capacity = message.ReadByte();
            for (var i = 0; i < Categories.Capacity; i++) {
                var id = message.ReadByte();
                var category = message.ReadString();
                Categories.Add((id, category));
            }
            SelectedCategory = message.ReadByte();
			
            CurrentPage = message.ReadUInt16();
            NumberOfPages = message.ReadUInt16();
			
            Entries.Capacity = message.ReadByte();
            for (var i = 0; i < Entries.Capacity; i++) {
                var rank = message.ReadUInt32();
                var character = message.ReadString();
                var unknownString = message.ReadString();
                var vocationId = message.ReadByte();
                var world = message.ReadString();
                var level = message.ReadUInt16();
                var isCurrentPlayer = message.ReadBool();
                var points = message.ReadUInt64();
                Entries.Add((rank, character, unknownString, vocationId, world, level, isCurrentPlayer, points));
            }
			UnknownByte1 = message.ReadByte();
			UnknownByte2 = message.ReadByte();
			UnknownByte3 = message.ReadByte();
            LastUpdateTimestamp = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Highscores);
			
            message.Write(NoData);
			if (NoData)
				return;
			
            message.Write((byte)GameWorlds.Capacity);
			foreach (string world in GameWorlds)
				message.Write(world);
			
            message.Write(SelectedWorld);
			
			message.Write(WorldCategory);
			message.Write(BattleEye);
			
            message.Write((byte)VocationOptions.Capacity);
			foreach (var profession in VocationOptions) {
				message.Write(profession.Id);
				message.Write(profession.Vocation);
			}
            message.Write(SelectedVocation);
			
            message.Write((byte)Categories.Capacity);
			foreach (var category in Categories) {
				message.Write(category.Id);
				message.Write(category.Category);
			}
            message.Write(SelectedCategory);
			
            message.Write(CurrentPage);
            message.Write(NumberOfPages);
			
            message.Write((byte)Entries.Capacity);
			foreach (var entry in Entries) {
                message.Write(entry.Rank);
                message.Write(entry.Character);
                message.Write(entry.UnknownString);
                message.Write(entry.VocationId);
                message.Write(entry.World);
                message.Write(entry.Level);
                message.Write(entry.IsCurrentPlayer);
                message.Write(entry.Points);
			}
			
			message.Write(UnknownByte1);
			message.Write(UnknownByte2);
			message.Write(UnknownByte3);
            message.Write(LastUpdateTimestamp);
        }
    }
}
