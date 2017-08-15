using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO
{
    public static class Config
    {
        /// <summary>
        /// The TCP port to run the server on (default: 55092)
        /// </summary>
        public static int ServerPort = 6609;

        /// <summary>
        /// How often the server should process packets (default: 400ms)
        /// </summary>
        public static int TickTime = 400; //ffxi - 416;

        public static int PlayerUpdateDistance = 6;
        public static int NpcUpdateDistance = 6;

        public static DebugLevel DebugLevel = DebugLevel.Information | DebugLevel.Packet | DebugLevel.Warning | DebugLevel.Verbose;

        /// <summary>
        /// Box width/height of a zone
        /// </summary>
        public static int ZoneSize = 600;
    }

    [Flags]
    public enum DebugLevel
    {
        None = 0,
        Information = 1,
        Debug = 2,
        Warning = 4,
        Error = 6, 
        Critical = 8,
        Packet = 16,
        Verbose = 32
    }
}
