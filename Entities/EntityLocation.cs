using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.World;

namespace MMO.Entities
{
    public class EntityLocation
    {

        /// <summary>
        /// Zone
        /// </summary>
        public AreaName Area { get; set; }

        /// <summary>
        /// Current X
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Current Y
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Current Z
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Zone Instance
        /// </summary>
        public int Instance { get; set; }

        /// <summary>
        /// Intended Destination X
        /// </summary>
        public int DestX { get; set; }

        /// <summary>
        /// Intended Destination Y
        /// </summary>
        public int DestY { get; set; }

        /// <summary>
        /// Intended Destination Z
        /// </summary>
        public int DestZ { get; set; }
    }
}
