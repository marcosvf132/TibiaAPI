using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using System;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetOutfit : ClientPacket
    {
        public uint TryOutfitKey { get; set; }

        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }
        public ushort MountOutfitId { get; set; }
        public ushort FamiliarOutfitId { get; set; }
        public ushort PodiumOutfitId { get; set; }

        public byte Type { get; set; }
        public byte Addons { get; set; }
        public byte DetailColor { get; set; }
        public byte HeadColor { get; set; }
        public byte LegsColor { get; set; }
        public byte TorsoColor { get; set; }
        public byte MountDetailColor { get; set; }
        public byte MountHeadColor { get; set; }
        public byte MountLegsColor { get; set; }
        public byte MountTorsoColor { get; set; }
        public byte PodiumStackPos { get; set; }
        public byte PodiumDetailColor { get; set; }
        public byte PodiumHeadColor { get; set; }
        public byte PodiumLegsColor { get; set; }
        public byte PodiumTorsoColor { get; set; }
        public byte PodiumDirection { get; set; }
        public byte PodiumVisibility { get; set; }

        public bool RandomMount { get; set; }

        public Position PodiumPosition { get; set; }

        public SetOutfit(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SetOutfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = message.ReadByte();
            OutfitId = message.ReadUInt16();
            HeadColor = message.ReadByte();
            TorsoColor = message.ReadByte();
            LegsColor = message.ReadByte();
            DetailColor = message.ReadByte();
            Addons = message.ReadByte();

            if ((OutfitWindowType)Type == OutfitWindowType.SelectOutfit) {
                MountOutfitId = message.ReadUInt16();
                MountHeadColor = message.ReadByte();
                MountTorsoColor = message.ReadByte();
                MountLegsColor = message.ReadByte();
                MountDetailColor = message.ReadByte();
                FamiliarOutfitId = message.ReadUInt16();
            } else if ((OutfitWindowType)Type == OutfitWindowType.TryOutfitMount) {
                TryOutfitKey = message.ReadUInt32();
            } else if ((OutfitWindowType)Type == OutfitWindowType.TryMountOld) {
                PodiumPosition = message.ReadPosition();
                PodiumOutfitId = message.ReadUInt16();
                PodiumStackPos = message.ReadByte();
                PodiumHeadColor = message.ReadByte();
                PodiumTorsoColor = message.ReadByte();
                PodiumLegsColor = message.ReadByte();
                PodiumDetailColor = message.ReadByte();
                PodiumDirection = message.ReadByte();
                PodiumVisibility = message.ReadByte();
            } else {
                throw new Exception($"Invalid SetOutfit type: {Type}");
            }
            RandomMount = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetOutfit);
            message.Write(Type);
            message.Write(OutfitId);
            message.Write(HeadColor);
            message.Write(TorsoColor);
            message.Write(LegsColor);
            message.Write(DetailColor);
            message.Write(Addons);

            if ((OutfitWindowType)Type == OutfitWindowType.SelectOutfit) {
                message.Write(MountOutfitId);
                message.Write(MountHeadColor);
                message.Write(MountTorsoColor);
                message.Write(MountLegsColor);
                message.Write(MountDetailColor);
                message.Write(FamiliarOutfitId);
            } else if ((OutfitWindowType)Type == OutfitWindowType.TryOutfitMount) {
                message.Write(TryOutfitKey);
            } else if ((OutfitWindowType)Type == OutfitWindowType.TryMountOld) {
                message.Write(PodiumPosition);
                message.Write(PodiumOutfitId);
                message.Write(PodiumStackPos);
                message.Write(PodiumHeadColor);
                message.Write(PodiumTorsoColor);
                message.Write(PodiumLegsColor);
                message.Write(PodiumDetailColor);
                message.Write(PodiumDirection);
                message.Write(PodiumVisibility);
            }
            message.Write(RandomMount);
        }
    }
}
