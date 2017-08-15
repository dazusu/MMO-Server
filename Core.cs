using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MMO.Entities;
using MMO.Networking;
using MMO.Packets;
using MMO.World;
using SocketMessaging;
using Timer = System.Timers.Timer;

namespace MMO
{
    public enum CoreState
    {
        Uninitialized,
        Initialized,
        Running,
        Stopped,
        Error

    }
    /// <summary>
    /// 
    /// </summary>
    public static class Core
    {
        #region Private Fields
        private static object _syncLock = new object();
        private static volatile int _tickCount = 0;
        private static Thread _tick;
        private static CoreState _state = CoreState.Uninitialized;
        /// <summary>
        /// Used for timing how long a tick takes to complete.
        /// </summary>
        private static Stopwatch _tickProcessStopwatch = new Stopwatch();
        private static Timer _tickTimer = new Timer();
        // Contains references to all players on the server.
        private static List<IEntity> _players;
        // Contains references to all enemies on the server.
        private static List<IEntity> _enemies;
        // Contains references to all zones on the server.
        private static List<Zone> _zones;
        # endregion

        #region Public Properties

        public static List<IEntity> Players => _players;
        public static List<Zone> Zones => _zones;

        #endregion //Public Properties

        #region Public Methods

        /// <summary>
        /// Starts up the MMO Core. Call Start() to begin Core Ticks.
        /// </summary>
        /// <returns></returns>
        public static bool Initialize()
        {
            Debug.WriteInfo("Initializing Core...");

            // Create Players and Zones Collections.
            _players = new List<IEntity>();
            _zones = new List<Zone>();

            Debug.WriteInfo("Created entity list.");
            Debug.WriteInfo("Created zone list.");

            // Load Zones.
            // ZoneManager.Initialize();
            _zones.Add(new Zone()
            {
                Area = AreaName.UngurForest
            });
            _zones.Add(new Zone()
            {
                Area = AreaName.VertholtPlains
            });
            Debug.WriteInfo($"Loaded {_zones.Count()} zones.");

            // Load Enemies.


            // Update Core State
            _state = CoreState.Initialized;
            Debug.WriteInfo("Initialization complete.");
            return true;
        }

        /// <summary>
        /// Starts Network Server & Starts Core Ticks being processed.
        /// </summary>
        /// <returns></returns>
        public static bool Start()
        {
            // Check that the core has been intialized before starting, and isn't already running.
            if (_state == CoreState.Uninitialized)
            {
                throw new Exception("Core must be initialized before calling Start()");
            }
            if (_state == CoreState.Running)
            {
                throw new Exception("Core is already running.");
            }

            // Update core state to Running.
            _state = CoreState.Running;

            // Starts up the Network Server.
            Server.Connection += Server_Connection;
            Server.ClientPacket += Server_ClientPacket;
            Server.Start(Config.ServerPort);

            // Start the server's logic Ticking.
            //_tick = new Thread(ServerTick);
            //_tick.Start();

            _tickTimer.Elapsed += new ElapsedEventHandler(ServerTick);
            _tickTimer.Interval = Config.TickTime;
            _tickTimer.Enabled = true;
            GC.KeepAlive(_tickTimer);

            return true;
        }

        /// <summary>
        /// Returns UTC Time.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTimestamp()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Get amount of miliseconds elapsed into current Tick.
        /// </summary>
        /// <returns></returns>
        public static long GetTickMiliseconds()
        {
            return Config.TickTime - _tickProcessStopwatch.ElapsedMilliseconds;
        }

        #endregion //Public Methods

        #region Packet Events
        /// <summary>
        /// Event that is fired when an incoming packet is recieved from a Player.
        /// </summary>
        /// <param name="container">A class containing the packet, and information about it's source.</param>
        private static void Server_ClientPacket(PacketContainer container)
        {
            if (container.PacketType == PacketType.Login)
            {
                LoginPacket p = (LoginPacket) container.Packet;

                // Handle Login Packet
                lock (_syncLock)
                {
                    // Load new character
                    IEntity character = new Character(p.EntityId);
                    character.ConnectionId = container.ConnectionId;

                    // Add character to global entity list.
                    _players.Add(character);

                    Debug.WritePacket($">>[{character.Name}] Login Args(Entity:{character.Id})");

                    // Add character to zone entity list.
                    _zones.Single(x => x.Area == character.Location.Area).PlayerZoneIn(character);

                }
            }
            else
            {
                // Send to Character
                _players.Single(c => c.ConnectionId == container.ConnectionId).ProcessPacket(container.Packet);
            }
        }

        /// <summary>
        /// Event that is fired when a player connects to the Network server.
        /// </summary>
        /// <param name="c">The connection belonging to the player.</param>
        private static void Server_Connection(Connection c)
        {

        }
        #endregion

        #region Server Ticks

        /// <summary>
        /// Processes all packets in the queue every 250s, and sends them out to clients.
        /// </summary>
        public static void ServerTick(object sender, ElapsedEventArgs e)
        {
            // Stop the tick timer.
            _tickTimer.Stop();
            // Increment Tick Counter
            _tickCount++;


            // ==============================================================================
            _tickProcessStopwatch.Restart();
            // ==============================================================================



            // Packets to be sent at the end of this tick.
            List<BulkPacket> bulkPackets = new List<BulkPacket>();

            bulkPackets.AddRange(ZoneTick.EntityUpdates());

            // Push all packets to clients.
            Server.PushPacket(bulkPackets);


            
            // ==============================================================================
            _tickProcessStopwatch.Stop();
            // ==============================================================================


            //Debug.WriteVerbose($"Tick {_tickCount} processed in {_tickProcessStopwatch.ElapsedMilliseconds}ms");

            _tickTimer.Interval = (double)Math.Max(Config.TickTime - _tickProcessStopwatch.ElapsedMilliseconds, 100);
            _tickTimer.Start();
        }

        #endregion
    }

    /// <summary>
    /// An update packet for a single player. Can contain multiple individual packets.
    /// </summary>
    public class BulkPacket
    {
        public int[] TargetConnectionIds { get; set; }
        public List<IPacket> Packets { get; set; }

        public BulkPacket(List<IPacket> packets, params int[] connectionId)
        {
            Packets = packets;
            TargetConnectionIds = connectionId;
        }
    }
}