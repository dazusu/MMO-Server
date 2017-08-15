using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Packets
{
    public class MoveToDestPacket : Packet
    {
        public int CurrentX;
        public int CurrentY;
        public int CurrentZ;
        public int DestX;
        public int DestY;
        public int DestZ;
        public int Speed;
    }
}
