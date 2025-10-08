
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DB_Manager.Helpers
{
    internal static class ConsoleInput
    {
        /*
                var k = Console.ReadKey(intercept: true);
               Captura una sola tecla.
               En este caso es la primera letra del nombre de la BBDD, o puede ser ESC si el usuario quiere cancelar.
               string? tail = Console.ReadLine();
               Después de leer esa primera tecla (k.KeyChar), el usuario sigue escribiendo.
               Console.ReadLine() lee el resto del texto hasta que pulsa ENTER.
               tail es esa “cola” del texto.
               El ? significa que puede ser null (por ejemplo si solo pulsó ENTER sin nada).
               string fileName = (k.KeyChar + (tail ?? "")).Trim();
               k.KeyChar = el primer carácter.
               (tail ?? "") = si tail es null, lo sustituye por cadena vacía.
               Se suman: k.KeyChar + tail → esto forma la cadena completa que el usuario tecleó (primera letra + resto).
        */

        public static string? ReadLineOrEscape(string prompt = "")
        {
            if (!string.IsNullOrEmpty(prompt)) { Console.WriteLine(prompt); }

            while (true)
            {

                var first = Console.ReadKey(intercept: true);

                if (first.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Operación cancelada.");
                    ConsolePause.Wait();
                    return null; }

                Console.Write(first.KeyChar);

                string? rest = Console.ReadLine();
                string input = (first.KeyChar.ToString() + (rest ?? "")).Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Entrada vacía. Inténtelo de nuevo:");
                    continue;
                }

                return input;
            }

        }

        public static bool? Confirmation(string question)
        {
            while (true)
            {
                Console.WriteLine($"{question} (S/N, ESC para cancelar).");

                var key = Console.ReadKey();

                Console.WriteLine();

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Operación cancelada.");
                    ConsolePause.Wait();
                    return null;
                }
                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                if (key.Key == ConsoleKey.N) { return false; }

                Console.WriteLine("Entrada no válida.");
                ConsolePause.Wait();
                continue;
            }           
        }
    }
}
