using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.World;

namespace MMO.Packets
{
    public class ProfilePacket : Packet
    {
        public string Name;
        public int CurrentX;
        public int CurrentY;
        public int CurrentZ;
        public AreaName Zone;

        public ProfilePacket()
        {
            PacketType = PacketType.Profile;
        }
    }
}
