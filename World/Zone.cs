using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;

namespace MMO.World
{
    public class Zone
    {
        private static object _syncLock = new object();

        // The area this zone represents.
        public AreaName Area { get; set; }

        // Players residing in the current zone.
        public List<IEntity> Players { get; set; }

        public List<IEntity> Enemies { get; set; }

        public Zone()
        {
            Players = new List<IEntity>();
            Enemies = new List<IEntity>();
        }

        public void PlayerZoneIn(IEntity character)
        {
            lock (_syncLock)
            {
                Players.Add(character);
                Debug.WriteInfo($"{Area} PlayerZoneIn => {character.Name}");
            }
        }

        public void PlayerZoneOut(IEntity character)
        {
            lock(_syncLock)
            {
                Players.Remove(character);
                Debug.WriteInfo($"{Area} PlayerZoneOut => {character.Name}");
            }
        }
    }
}
