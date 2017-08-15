using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO.Entities
{
    /// <summary>
    /// Defines the type of entity.
    /// </summary>
    public enum EntityType
    {
        Unassigned,
        PlayerCharacter,
        NonPlayerCharacter,
        Enemy,
        Object,
        Pet
    }
}
