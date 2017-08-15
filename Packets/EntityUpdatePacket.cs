using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;

namespace MMO.Packets
{
    /// <summary>
    /// Outgoing packet. Updates general information about entities around the player.
    /// </summary>
    public class EntityUpdatePacket : Packet
    {
        public int Index;
        public string Name;
        public EntityType Type;
        public EntityStatus Status;
        public int CurrentX;
        public int CurrentY;
        public int CurrentZ;
        public EntityHeading Heading;

        public EntityUpdatePacket()
        {
            PacketType = PacketType.EntityUpdate;
        }
    }
}
