using System;
using System.Collections.Generic;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Outfit : ServerPacket
    {
        public List<(ushort FemaleLooktype, ushort MaleLooktype)> HirelingDresses { get; } =
            new List<(ushort FemaleLooktype, ushort MaleLooktype)>();
        public List<(ushort Id, string Name, byte Addons, byte ButtonType, uint StoreOfferId)> Outfits { get; } =
            new List<(ushort Id, string Name, byte Addons, byte ButtonType, uint StoreOfferId)>();
        public List<(ushort Id, string Name, byte ButtonType, uint StoreOfferId)> Mounts { get; } =
            new List<(ushort Id, string Name, byte ButtonType, uint StoreOfferId)>();
        public List<(ushort Id, string Name, byte ButtonType, uint StoreOfferId)> Familiars { get; } =
            new List<(ushort Id, string Name, byte ButtonType, uint StoreOfferId)>();

		public Appearances.OutfitInstance OutfitInst { get; set; }
		public Appearances.OutfitInstance MountInst { get; set; }

        public ushort Familiar { get; set; }
        public ushort UnknownUint16 { get; set; }
        public ushort Id { get; set; }

        public bool IsMounted { get; set; }
        public bool IsVisible { get; set; }
        public bool IsOutfit { get; set; }
        public bool isRandomMount { get; set; }

        public byte StackPos { get; set; }
        public byte Direction { get; set; }
        public byte Type { get; set; }
		
        public Position PodiumPosition { get; set; }
		
        public Outfit(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Outfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OutfitInst = (Appearances.OutfitInstance)message.ReadCreatureOutfit();
            MountInst = (Appearances.OutfitInstance)message.ReadMountOutfit(true);
			Familiar = message.ReadUInt16();

            Outfits.Capacity = message.ReadUInt16();
            for (var i = 0; i < Outfits.Capacity; ++i) {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var addons = message.ReadByte();
                var buttonType = message.ReadByte();
                uint storeOfferId = 0;
                if (buttonType == 0x01)
                    storeOfferId = message.ReadUInt32();
                Outfits.Add((id, name, addons, buttonType, storeOfferId));
            }

            Mounts.Capacity = message.ReadUInt16();
            for (var i = 0; i < Mounts.Capacity; ++i) {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var buttonType = message.ReadByte();
                uint storeOfferId = 0;
                if (buttonType == 0x01)
                    storeOfferId = message.ReadUInt32();
                Mounts.Add((id, name, buttonType, storeOfferId));
            }

            Familiars.Capacity = message.ReadUInt16();
            for (var i = 0; i < Familiars.Capacity; ++i) {
                var id = message.ReadUInt16();
                var name = message.ReadString();
                var buttonType = message.ReadByte();
                uint storeOfferId = 0;
                if (buttonType == 0x01)
                    storeOfferId = message.ReadUInt32();
                Familiars.Add((id, name, buttonType, storeOfferId));
            }

			Type = message.ReadByte();
            if (Type == (byte)OutfitWindowType.SelectOutfit) {
                IsMounted = message.ReadBool();
				isRandomMount = message.ReadBool();
            } else if (Type == (byte)OutfitWindowType.TryPodium) {
                IsMounted = message.ReadBool();

                UnknownUint16 = message.ReadUInt16();
                PodiumPosition = message.ReadPosition();
                Id = message.ReadUInt16();
                StackPos = message.ReadByte();

                IsVisible = message.ReadBool();
                IsOutfit = message.ReadBool();
                Direction = message.ReadByte();
            } else if (Type == (byte)OutfitWindowType.TryHirelingDress) {
                HirelingDresses.Capacity = message.ReadUInt16();
                for (var i = 0; i < HirelingDresses.Capacity; ++i) {
                    var femaleLooktype = message.ReadUInt16();
                    var maleLooktype = message.ReadUInt16();
                    HirelingDresses.Add((femaleLooktype, maleLooktype));
                }
            } else {
				throw new Exception($"[Outfit.ParseFromNetworkMessage] Unknown outfit window type '{Type}'.");
			}
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Outfit);
            message.Write(OutfitInst);
            message.Write(MountInst);
			message.Write(Familiar);

            var count = Math.Min(Outfits.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (Id, Name, Addons, ButtonType, StoreOfferId) = Outfits[i];
                message.Write(Id);
                message.Write(Name);
                message.Write(Addons);
                message.Write(ButtonType);
                if (ButtonType == 0x01)
					message.Write(StoreOfferId);
            }

            count = Math.Min(Mounts.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i) {
                var (Id, Name, ButtonType, StoreOfferId) = Mounts[i];
                message.Write(Id);
                message.Write(Name);
                message.Write(ButtonType);
                if (ButtonType == 0x01)
					message.Write(StoreOfferId);
            }

			message.Write(Type);
			if (Type == (byte)OutfitWindowType.SelectOutfit) {
				message.Write(IsMounted);
				message.Write(isRandomMount);
			} else if (Type == (byte)OutfitWindowType.TryPodium) {
				message.Write(IsMounted);
				
				message.Write(UnknownUint16);
				message.Write(PodiumPosition);
				message.Write(Id);
				message.Write(StackPos);
				
				message.Write(IsVisible);
				message.Write(IsOutfit);
				message.Write(Direction);
            } else if (Type == (byte)OutfitWindowType.TryHirelingDress) {
                message.Write((ushort)HirelingDresses.Capacity);
                foreach (var hireling in HirelingDresses) {
                    message.Write(hireling.FemaleLooktype);
                    message.Write(hireling.MaleLooktype);
                }
            }
        }
    }
}
