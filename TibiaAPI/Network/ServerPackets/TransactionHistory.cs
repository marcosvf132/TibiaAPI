using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TransactionHistory : ServerPacket
    {
        public List<(uint Id, uint Timestamp, byte Unknown1, int CreditChange, byte Unknown2, string Description, bool HasDetails)> NewTransactions { get; } =
            new List<(uint Id, uint Timestamp, byte Unknown1, int CreditChange, byte Unknown2, string Description, bool HasDetails)>();
        public List<(uint Timestamp, byte Type, int CreditChange, string Name)> Transactions { get; } =
            new List<(uint Timestamp, byte Type, int CreditChange, string Name)>();

        public uint CurrentPage { get; set; }
        public uint NumberOfPages { get; set; }

        public TransactionHistory(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TransactionHistory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CurrentPage = message.ReadUInt32();
            NumberOfPages = message.ReadUInt32();

            var count = message.ReadByte();
            NewTransactions.Capacity = count;

            for (var i = 0; i < count; ++i) {
				var id = message.ReadUInt32();
				var timestamp = message.ReadUInt32();
				var unknown1 = message.ReadByte();
				var creditChange = message.ReadInt32();
				var unknown2 = message.ReadByte();
				var description = message.ReadString();
				var hasDetails = message.ReadBool();
				NewTransactions.Add((id, timestamp, unknown1, creditChange, unknown2, description, hasDetails));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TransactionHistory);
            message.Write(CurrentPage);
            message.Write(NumberOfPages);
            var count = Math.Min(NewTransactions.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i) {
				var (Id, Timestamp, Unknown1, CreditChange, Unknown2, Description, HasDetails) = NewTransactions[i];
				message.Write(Id);
				message.Write(Timestamp);
				message.Write(Unknown1);
				message.Write(CreditChange);
				message.Write(Unknown2);
				message.Write(Description);
				message.Write(HasDetails);
            }
        }
    }
}
