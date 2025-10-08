using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DB_Manager.Helpers
{
    internal static class ConsolePause
    {
        public static void Wait(string message = "Pulse cualquier tecla para continuar...")
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey(intercept: true);
        }
    }
}
