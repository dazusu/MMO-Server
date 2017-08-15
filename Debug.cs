using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMO
{
    public static class Debug
    {
        private const string DateTimeFormat = "HH:mm:ss";

        public static void WriteError(string message)
        {
            if (!Config.DebugLevel.HasFlag(DebugLevel.Error)) return;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][Error   ] {message}");
        }
        public static void WriteInfo(string message)
        {
            if(!Config.DebugLevel.HasFlag(DebugLevel.Information)) return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][Info    ] {message}");
        }

        public static void WriteCritical(string message)
        {
            if(!Config.DebugLevel.HasFlag(DebugLevel.Critical)) return;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][Critical] {message}");
        }

        public static void WritePacket(string message)
        {
            if(!Config.DebugLevel.HasFlag(DebugLevel.Packet)) return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][Packet  ] {message}");
        }

        public static void WriteVerbose(string message)
        {
            if (!Config.DebugLevel.HasFlag(DebugLevel.Verbose)) return;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss fff}ms][Verbose ] {message}");
        }

        public static void WriteWarning(string message)
        {
            if(!Config.DebugLevel.HasFlag(DebugLevel.Warning)) return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}][Warning ] {message}");
        }

    }
}
