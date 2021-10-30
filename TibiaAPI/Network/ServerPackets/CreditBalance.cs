using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreditBalance : ServerPacket
    {
        public int TransferableTibiaCoins { get; set; }
        public int TotalTibiaCoins { get; set; }
        public int AuctionCoins { get; set; }
        public int TournamentCoins { get; set; }

        public bool UpdateCreditBalance { get; set; }

        public CreditBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreditBalance;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            UpdateCreditBalance = message.ReadBool();
            if (UpdateCreditBalance) {
                TotalTibiaCoins = message.ReadInt32();
                TransferableTibiaCoins = message.ReadInt32();
                AuctionCoins = message.ReadInt32();
                TournamentCoins = message.ReadInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreditBalance);
            message.Write(UpdateCreditBalance);
            if (UpdateCreditBalance) {
                message.Write(TotalTibiaCoins);
                message.Write(TransferableTibiaCoins);
                message.Write(AuctionCoins);
                message.Write(TournamentCoins);
            }
        }
    }
}
