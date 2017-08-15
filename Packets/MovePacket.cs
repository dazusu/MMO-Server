using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;

namespace MMO.Packets
{
    public class MovePacket : Packet
    {
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public int CurrentZ { get; set; }
        public EntityHeading Heading { get; set; }

        public MovePacket()
        {
            PacketType = PacketType.Move;
        }
    }
}
