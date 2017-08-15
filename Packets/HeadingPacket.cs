using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;

namespace MMO.Packets
{
    public class HeadingPacket : Packet
    {
        public EntityHeading Heading;

        public HeadingPacket()
        {
            PacketType = PacketType.Heading;
        }
    }
}
