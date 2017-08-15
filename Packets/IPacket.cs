using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Packets
{
    // Packet interface.
    public interface IPacket
    {
        PacketType PacketType { get; }
    }
}
