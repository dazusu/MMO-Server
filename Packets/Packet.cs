using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Packets
{
    // Shared packet properties.
    public abstract class Packet : IPacket
    {
        public int EntityId { get; set; }
        public PacketType PacketType { get; set; }
    }

    // Packet types.
    public enum PacketType
    {
        Login = 0,
        Logout = 1,
        MoveToDest = 2,
        Move = 3,
        EntityUpdate = 4,
        Heading = 5
    }
}
