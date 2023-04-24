using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    public class Log
    {
        public static void Normal(string message)
        { 
            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine($"[MSG] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
