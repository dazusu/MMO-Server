using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MMO.Packets
{
    public class LoginPacket : Packet
    {
        public string LoginHash { get; set; }
    }
}
