using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Networking;
using MMO.Packets;

namespace MMO.Entities
{
    public interface IEntity
    {
        int ConnectionId { get; set; }
        int Id { get; }
        string Name { get; set; }
        EntityType Type { get; }
        EntityStatus Status { get; }
        EntityUpdateFlag EntityUpdateFlag { get; }
        EntityStats Stats { get; }
        EntityLocation Location { get; }
        void ProcessPacket(IPacket packet);
        EntityUpdatePacket GetEntityUpdatePacket();
        void RemoveUpdateFlag(EntityUpdateFlag flag);
    }
}
