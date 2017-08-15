using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Packets;

namespace MMO.Entities
{
    public class Enemy : Entity
    {
        public override void ProcessPacket(IPacket packet)
        {
            throw new NotImplementedException();
        }

        public override EntityUpdatePacket GetEntityUpdatePacket()
        {
            _updateFlag &= ~EntityUpdateFlag.EntityUpdate;

            return new EntityUpdatePacket()
            {
                Id = Id,
                Name = Name,
                Type = Type,
                Status = Status,
                CurrentX = Location.X,
                CurrentY = Location.Y,
                CurrentZ = Location.Z,
            };
        }
    }
}
