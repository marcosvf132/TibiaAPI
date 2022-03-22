using OXGaming.TibiaAPI.Constants;
using System;
using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PartyHuntAnalyser : ClientPacket
    {
        public byte Type { get; set; }

        public List<(ushort ItemId, ulong Price)> ItemsData { get; } =
            new List<(ushort ItemId, ulong Price)>();

        public PartyHuntAnalyser(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PartyHuntAnalyser;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            if ((PartyAnalyzerType)Type == PartyAnalyzerType.PriceValue) {
                ItemsData.Capacity = message.ReadUInt16();
                for (var i = 0; i < ItemsData.Capacity; ++i)
                {
                    var itemId = message.ReadUInt16();
                    var price = message.ReadUInt64();
                    ItemsData.Add((itemId, price));
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PartyHuntAnalyser);
            if ((PartyAnalyzerType)Type == PartyAnalyzerType.PriceValue) {
                var count = Math.Min(byte.MaxValue, ItemsData.Count);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i) 
                {
                    var (ItemId, Price) = ItemsData[i];
                    message.Write(ItemId);
                    message.Write(Price);
                }
            }
        }
    }
}
