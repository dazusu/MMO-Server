using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;
using MMO.Packets;

namespace MMO.World
{
    public static class ZoneTick
    {
        /// <summary>
        /// Gets EntityUpdate Packets for entities surrounding each player on the server.
        /// </summary>
        /// <returns></returns>
        public static List<BulkPacket> EntityUpdates()
        {
            // Create a list of bulk packets for all zones. (One per zone).
            List<BulkPacket> bulks = new List<BulkPacket>();

            // Updates character entities for each zone that has players in it.
            foreach (Zone zone in Core.Zones.Where(z => z.Players.Any()))
            {
                foreach (IEntity character in zone.Players)
                {
                    List<IPacket> entityUpdates = GetZoneEntityUpdates(zone, character);

                    if (!entityUpdates.Any())
                        continue;

                    BulkPacket bulk = new BulkPacket(entityUpdates, character.ConnectionId);

                    bulks.Add(bulk);
                }

                zone.Players.ForEach(x => x.RemoveUpdateFlag(EntityUpdateFlag.EntityUpdate));
            }

            return bulks;
        }

        /// <summary>
        /// Get updates for all entities around a specific location.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="character">The location surrounding.</param>
        /// <returns></returns>
        private static List<IPacket> GetZoneEntityUpdates(Zone zone, IEntity character)
        {
            List<IPacket> playerUpdatePackets = new List<IPacket>(zone.Players
                .Where(x => x.Id != character.Id && x.EntityUpdateFlag.HasFlag(EntityUpdateFlag.EntityUpdate) && MathUtilities.Distance(character.Location, x.Location) < Config.PlayerUpdateDistance)
                .Select(y => y.GetEntityUpdatePacket()).ToList());

            List<IPacket> enemyUpdatePackets = new List<IPacket>(zone.Enemies
                .Where(x => x.EntityUpdateFlag.HasFlag(EntityUpdateFlag.EntityUpdate) && MathUtilities.Distance(character.Location, x.Location) < Config.PlayerUpdateDistance)
                .Select(y => y.GetEntityUpdatePacket()).ToList());

            var r = playerUpdatePackets.Concat(enemyUpdatePackets);

            return r.ToList();
        }

        /// <summary>
        /// Gets "Update Packets" for players in a given zone that require a refresh.
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        private static List<IPacket> GetPlayerEntityUpdates(Zone zone)
        {
            return new List<IPacket>(zone.Players
                .Where(p => p.EntityUpdateFlag.HasFlag(EntityUpdateFlag.EntityUpdate))
                .Select(s => s.GetEntityUpdatePacket())
                .ToList());
        }
    }

}
