using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQLite_DB_Manager.Helpers
{
    internal static class DbBrowser
    {
        /// <summary>
        /// Muestra las BBDD numeradas y devuelve el array de rutas.
        /// </summary>

        public static string[] ListAndPrint()
        {
            var files = Storage.ListDataBases().ToArray();

            if (files.Length == 0)
            {
                Console.WriteLine("No hay BBDD que mostrar.");
                ConsolePause.Wait();
                return Array.Empty<string>();
            }

            Console.WriteLine("\nBBDD disponibles");

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {Path.GetFileName(files[i])}");
            }           
            return files;
        }

        /// <summary>
        /// Lee el índice del teclado. ESC para cancelar.
        /// </summary>
         
        public static int ReadIndexOrEscape(int max)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var k = Console.ReadKey(intercept: true);

                
                if (k.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Operación cancelada.");
                    ConsolePause.Wait();
                    return -1;
                }
                if (k.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    if (int.TryParse(sb.ToString(), out int n) && n >= 1 && n <= max) return n - 1;

                    Console.WriteLine("Entrada no válida. Inténtelo de nuevo: ");
                    sb.Clear();
                    continue;
                }

                if (k.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        sb.Length--;
                        Console.Write("\b \b");
                    }
                    continue;
                }

                if(!char.IsControl(k.KeyChar) && char.IsDigit(k.KeyChar))
                {
                    sb.Append(k.KeyChar);
                    Console.Write(k.KeyChar);
                }
            }
        }
    }
}
