using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BosstiaryStaticData : ServerPacket
    {

        #region Kills
        public ushort BaneProwesKills { get; set; }
        public ushort BaneExpertiseKills { get; set; }
        public ushort BaneMasteryKills { get; set; }

        public ushort ArchfoeProwesKills { get; set; }
        public ushort ArchfoeExpertiseKills { get; set; }
        public ushort ArchfoeMasteryKills { get; set; }

        public ushort NemesisProwesKills { get; set; }
        public ushort NemesisExpertiseKills { get; set; }
        public ushort NemesisMasteryKills { get; set; }
        #endregion

        #region Points
        public ushort BaneProwesPoints { get; set; }
        public ushort BaneExpertisePoints { get; set; }
        public ushort BaneMasteryPoints { get; set; }

        public ushort ArchfoeProwesPoints { get; set; }
        public ushort ArchfoeExpertisePoints { get; set; }
        public ushort ArchfoeMasteryPoints { get; set; }

        public ushort NemesisProwesPoints { get; set; }
        public ushort NemesisExpertisePoints { get; set; }
        public ushort NemesisMasteryPoints { get; set; }
        #endregion
        public BosstiaryStaticData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BosstiaryStaticData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            BaneProwesKills = message.ReadUInt16();
            BaneExpertiseKills = message.ReadUInt16();
            BaneMasteryKills = message.ReadUInt16();
            ArchfoeProwesKills = message.ReadUInt16();
            ArchfoeExpertiseKills = message.ReadUInt16();
            ArchfoeMasteryKills = message.ReadUInt16();
            NemesisProwesKills = message.ReadUInt16();
            NemesisExpertiseKills = message.ReadUInt16();
            NemesisMasteryKills = message.ReadUInt16();
            BaneProwesPoints = message.ReadUInt16();
            BaneExpertisePoints = message.ReadUInt16();
            BaneMasteryPoints = message.ReadUInt16();
            ArchfoeProwesPoints = message.ReadUInt16();
            ArchfoeExpertisePoints = message.ReadUInt16();
            ArchfoeMasteryPoints = message.ReadUInt16();
            NemesisProwesPoints = message.ReadUInt16();
            NemesisExpertisePoints = message.ReadUInt16();
            NemesisMasteryPoints = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BosstiaryStaticData);
            message.Write(BaneProwesKills);
            message.Write(BaneExpertiseKills);
            message.Write(BaneMasteryKills);
            message.Write(ArchfoeProwesKills);
            message.Write(ArchfoeExpertiseKills);
            message.Write(ArchfoeMasteryKills);
            message.Write(NemesisProwesKills);
            message.Write(NemesisExpertiseKills);
            message.Write(NemesisMasteryKills);
            message.Write(BaneProwesPoints);
            message.Write(BaneExpertisePoints);
            message.Write(BaneMasteryPoints);
            message.Write(ArchfoeProwesPoints);
            message.Write(ArchfoeExpertisePoints);
            message.Write(ArchfoeMasteryPoints);
            message.Write(NemesisProwesPoints);
            message.Write(NemesisExpertisePoints);
            message.Write(NemesisMasteryPoints);
        }
    }
}
