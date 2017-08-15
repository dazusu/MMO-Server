using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;
using MMO.World;

namespace MMO
{
    public static class CoreUtilities
    {
        public static IEnumerable<IEntity> GetPlayersInZone(AreaName zone)
        {
            return Core.Players.Where(pc => pc.Location.Area == zone);
        }
    }
}
