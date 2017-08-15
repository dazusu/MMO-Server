using MMO.Jobs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Networking;
using MMO.Packets;
using MMO.World;

namespace MMO.Entities
{
    public class Character : Entity
    {
        public delegate void PacketHandler(PacketContainer packet);
        //public event PacketHandler Packet;

        /// <summary>
        /// Constructor that loads character by ID.
        /// </summary>
        /// <param name="id">ID of character to load.</param>
        public Character(int id)
        {
            Load(id);
        }

        /// <summary>
        /// Loads character from the database by ID.
        /// </summary>
        /// <param name="id">ID of character to load.</param>
        private void Load(int id)
        {
            if(id == 1)
            {
                Name = "Kapao";
                _id = 1;
                _location = new EntityLocation()
                {
                    Instance = 0,
                    X = 3,
                    Y = 3,
                    Z = 1,
                    Area = AreaName.UngurForest
                };
                _stats = new EntityStats(1, 1, 1, 1, 1, 1, 1);
                _existing = true;
                _type = EntityType.PlayerCharacter;
                _status = EntityStatus.Loading;
            }
            else if (id == 2)
            {
                Name = "Dazusu";
                _id = 2;
                _location = new EntityLocation()
                {
                    Instance = 0,
                    X = 5,
                    Y = 5,
                    Z = 1,
                    Area = AreaName.UngurForest
                };
                _stats = new EntityStats(1, 1, 1, 1, 1, 1, 1);
                _existing = true;
                _type = EntityType.PlayerCharacter;
                _status = EntityStatus.Loading;
            }
            else if(id == 3)
            {
                Name = "Jen";
                _id = 3;
                _location = new EntityLocation()
                {
                    Instance = 0,
                    X = 3,
                    Y = 4,
                    Z = 1,
                    Area = AreaName.UngurForest
                };
                _stats = new EntityStats(1, 1, 1, 1, 1, 1, 1);
                _existing = true;
                _type = EntityType.PlayerCharacter;
                _status = EntityStatus.Loading;
            }
            else if(id == 4)
            {
                Name = "Mike";
                _id = 4;
                _location = new EntityLocation()
                {
                    Instance = 0,
                    X = 3,
                    Y = 4,
                    Z = 1,
                    Area = AreaName.UngurForest
                };
                _stats = new EntityStats(1, 1, 1, 1, 1, 1, 1);
                _existing = true;
                _type = EntityType.PlayerCharacter;
                _status = EntityStatus.Loading;
            }
        }


        #region Incoming Client Packet Processing. (Private Methods)

        /// <summary>
        /// Process Incoming Packet
        /// </summary>
        /// <remarks>Forwards incoming packets to its own method for full processing.</remarks>
        /// <param name="packet">SmallPacket to process.</param>
        public override void ProcessPacket(IPacket packet)
        {
            switch(packet.PacketType)
            {
                case PacketType.Login:
                    HandleLoginPacket(packet);
                    break;
                case PacketType.MoveToDest:
                    HandleMoveToDestPacket(packet);
                    break;
                case PacketType.Move:
                    HandleMovePacket(packet);
                    break;
                case PacketType.Heading:
                    HandleHeadingPacket(packet);
                    break;
                default:
                    Debug.WriteError($"PacketType {packet.PacketType.ToString()} not supported.");
                    break;
            }
        }

        private void HandleMovePacket(IPacket packet)
        {
            MovePacket move = (MovePacket) packet;
            Location.X = move.CurrentX;
            Location.Y = move.CurrentY;
            Location.Z = move.CurrentZ;

            Debug.WritePacket($"{Name.PadRight(9, ' ')} {"Move".PadRight(10, ' ')} X:{Location.X.ToString().PadRight(3, ' ')} Y:{Location.Y.ToString().PadRight(3, ' ')} Z:{Location.Z.ToString().PadRight(3, ' ')}");

            _updateFlag |= EntityUpdateFlag.EntityUpdate;
        }

        private void HandleLoginPacket(IPacket packet)
        {
            LoginPacket login = (LoginPacket) packet;
        }

        private void HandleMoveToDestPacket(IPacket packet)
        {
            // Cast packet back to base place
            MoveToDestPacket moveToDest = (MoveToDestPacket) packet;

            // Update this player
            Location.X = moveToDest.CurrentX;
            Location.Y = moveToDest.CurrentY;
            Location.Z = moveToDest.CurrentZ;
            Location.DestX = moveToDest.DestX;
            Location.DestY = moveToDest.DestY;
            Location.DestZ = moveToDest.DestZ;

            Debug.WritePacket($"{Name.PadRight(9, ' ')} {"MoveToDest".PadRight(10, ' ')} X:{Location.X.ToString().PadRight(3, ' ')} Y:{Location.Y.ToString().PadRight(3, ' ')} Z:{Location.Z.ToString().PadRight(3, ' ')}");

            _updateFlag |= EntityUpdateFlag.EntityUpdate;
        }

        private void HandleHeadingPacket(IPacket packet)
        {
            HeadingPacket heading = (HeadingPacket) packet;
            Location.Heading = heading.Heading;

            _updateFlag |= EntityUpdateFlag.EntityUpdate;
        }
        #endregion

        #region Tick Packets

        public override EntityUpdatePacket GetEntityUpdatePacket()
        {
            return new EntityUpdatePacket()
            {
                Name = Name,
                Type = Type,
                Status = Status,
                CurrentX = Location.X,
                CurrentY = Location.Y,
                CurrentZ = Location.Z,
                EntityId = Id
            };
        }

        public override MovePacket GetEntityMovePacket()
        {
            return new MovePacket()
            {
                CurrentX = Location.X,
                CurrentY = Location.Y,
                CurrentZ = Location.Z,
                EntityId = Id
            };
        }

        #endregion


    }
}
