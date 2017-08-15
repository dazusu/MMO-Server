using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Entities
{
    [Flags]
    public enum EntityUpdateFlag
    {
        None,
        EntityUpdate,
        EntityMove
    }
}
