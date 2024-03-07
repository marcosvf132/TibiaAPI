using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ExaltationDialogRefresh : ServerPacket
    {

        public List<List<(ushort Item, byte Tier, ushort Amount)>> FusedItems { get; } =
            new List<List<(ushort Item, byte Tier, ushort Amount)>>();

        public List<List<(ushort Item, byte Tier, ushort Amount)>> ConvegenceFusedItems { get; } =
            new List<List<(ushort Item, byte Tier, ushort Amount)>>();

        public List<(List<(ushort Item, byte Tier, ushort Amount)> Senders, List<(ushort Item, ushort Amount)> Receivers)> TransferItems { get; } =
            new List<(List<(ushort Item, byte Tier, ushort Amount)> Senders, List<(ushort Item, ushort Amount)> Receivers)>();

        public List<(List<(ushort Item, byte Tier, ushort Amount)> Senders, List<(ushort Item, ushort Amount)> Receivers)> ConvegenceTransferItems { get; } =
            new List<(List<(ushort Item, byte Tier, ushort Amount)> Senders, List<(ushort Item, ushort Amount)> Receivers)>();

        public ushort MaxDust { get; set; }

        public ExaltationDialogRefresh(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ExaltationDialogRefresh;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            FusedItems.Capacity = message.ReadUInt16();
            for (var i = 0; i < FusedItems.Capacity; ++i) {
                var wear = new List<(ushort Item, byte Tier, ushort Amount)>
                {
                    Capacity = message.ReadByte()
                };
                for (var j = 0; j < wear.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var tier = message.ReadByte();
                    var amount = message.ReadUInt16();
                    wear.Add((item, tier, amount));
                }
                FusedItems.Add(wear);
            }
            
            ConvegenceFusedItems.Capacity = message.ReadUInt16();
            for (var i = 0; i < ConvegenceFusedItems.Capacity; ++i) {
                var wear = new List<(ushort Item, byte Tier, ushort Amount)>
                {
                    Capacity = message.ReadByte()
                };
                for (var j = 0; j < wear.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var tier = message.ReadByte();
                    var amount = message.ReadUInt16();
                    wear.Add((item, tier, amount));
                }
                ConvegenceFusedItems.Add(wear);
            }
            
            TransferItems.Capacity = message.ReadByte();
            for (var i = 0; i < TransferItems.Capacity; ++i) {
                var senders = new List<(ushort Item, byte Tier, ushort Amount)>
                {
                    Capacity = message.ReadUInt16()
                };
                for (var j = 0; j < senders.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var tier = message.ReadByte();
                    var amount = message.ReadUInt16();
                    senders.Add((item, tier, amount));
                }

                var receivers = new List<(ushort Item, ushort Amount)>
                {
                    Capacity = message.ReadUInt16()
                };
                for (var j = 0; j < receivers.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var amount = message.ReadUInt16();
                    receivers.Add((item, amount));
                }

                TransferItems.Add((senders, receivers));
            }
            
            ConvegenceTransferItems.Capacity = message.ReadByte();
            for (var i = 0; i < ConvegenceTransferItems.Capacity; ++i) {
                var senders = new List<(ushort Item, byte Tier, ushort Amount)>
                {
                    Capacity = message.ReadUInt16()
                };
                for (var j = 0; j < senders.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var tier = message.ReadByte();
                    var amount = message.ReadUInt16();
                    senders.Add((item, tier, amount));
                }

                var receivers = new List<(ushort Item, ushort Amount)>
                {
                    Capacity = message.ReadUInt16()
                };
                for (var j = 0; j < receivers.Capacity; ++j) {
                    var item = message.ReadUInt16();
                    var amount = message.ReadUInt16();
                    receivers.Add((item, amount));
                }

                ConvegenceTransferItems.Add((senders, receivers));
            }
            
            MaxDust = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((ushort)FusedItems.Capacity);
            for (var i = 0; i < FusedItems.Capacity; ++i) {
                var wear = FusedItems[i];
                message.Write((byte)wear.Capacity);
                for (var j = 0; j < wear.Capacity; ++j) {
                    var (Item, Tier, Amount) = wear[j];
                    message.Write(Item);
                    message.Write(Tier);
                    message.Write(Amount);
                }
            }
            
            message.Write((ushort)ConvegenceFusedItems.Capacity);
            for (var i = 0; i < ConvegenceFusedItems.Capacity; ++i) {
                var wear = ConvegenceFusedItems[i];
                message.Write((byte)wear.Capacity);
                for (var j = 0; j < wear.Capacity; ++j) {
                    var (Item, Tier, Amount) = wear[j];
                    message.Write(Item);
                    message.Write(Tier);
                    message.Write(Amount);
                }
            }
            
            message.Write((byte)TransferItems.Capacity);
            for (var i = 0; i < TransferItems.Capacity; ++i) {
                var (Senders, Receivers) = TransferItems[i];
                message.Write((ushort)Senders.Capacity);
                for (var j = 0; j < Senders.Capacity; ++j) {
                    var (Item, Tier, Amount) = Senders[j];
                    message.Write(Item);
                    message.Write(Tier);
                    message.Write(Amount);
                }
                message.Write((ushort)Receivers.Capacity);
                for (var j = 0; j < Receivers.Capacity; ++j) {
                    var (Item, Amount) = Receivers[j];
                    message.Write(Item);
                    message.Write(Amount);
                }
            }
            
            message.Write((byte)ConvegenceTransferItems.Capacity);
            for (var i = 0; i < ConvegenceTransferItems.Capacity; ++i) {
                var (Senders, Receivers) = ConvegenceTransferItems[i];
                message.Write((ushort)Senders.Capacity);
                for (var j = 0; j < Senders.Capacity; ++j) {
                    var (Item, Tier, Amount) = Senders[j];
                    message.Write(Item);
                    message.Write(Tier);
                    message.Write(Amount);
                }
                message.Write((ushort)Receivers.Capacity);
                for (var j = 0; j < Receivers.Capacity; ++j) {
                    var (Item, Amount) = Receivers[j];
                    message.Write(Item);
                    message.Write(Amount);
                }
            }
            
            message.Write(MaxDust);
        }
    }
}
