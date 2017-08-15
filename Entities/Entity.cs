using MMO.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MMO.Networking;
using MMO.Packets;
using MMO.World;

namespace MMO.Entities
{
    public abstract class Entity : IEntity
    {
        #region Fields
        protected bool _existing = false;
        protected int _id;
        protected EntityType _type;
        protected EntityStats _stats;
        protected EntityLocation _location;
        protected EntityStatus _status;
        protected bool _update = false;
        protected int _speed;
        protected EntityUpdateFlag _updateFlag;
        #endregion

        #region Properties
        public int ConnectionId { get; set; }
        public EntityType Type => _type;
        public int Id => _id;
        public string Name { get; set; }
        public EntityStats Stats => _stats;
        public EntityLocation Location => _location;
        public EntityStatus Status => _status;
        public EntityUpdateFlag EntityUpdateFlag => _updateFlag;
        #endregion

        #region Constructors

        protected Entity()
        {
            _type = EntityType.Unassigned;

            _stats = new EntityStats()
            {
                Str = 1,
                Dex = 1,
                Agi = 1,
                Vit = 1,
                Mnd = 1,
                Int = 1,
                Chr = 1
            };

            _location = new EntityLocation()
            {
                Instance = 0,
                X = 1,
                Y = 1,
                Z = 1,
                Area = AreaName.UngurForest,
                Heading = EntityHeading.None
            };

        }
        #endregion

        #region Public Methods

        public abstract void ProcessPacket(IPacket packet);
        public abstract EntityUpdatePacket GetEntityUpdatePacket();
        public virtual void RemoveUpdateFlag(EntityUpdateFlag flag)
        {
            _updateFlag &= ~flag;
        }

        #endregion


        #region Private Methods

        #endregion
    }
}
