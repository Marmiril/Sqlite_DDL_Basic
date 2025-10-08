using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLite_DB_Manager.Helpers;
using System.IO;

namespace SQLite_DB_Manager.DDL
{
    internal static class ModifyTable
    {
        public static void Execute(string dbPath)
        {
            string dbName = Path.GetFileName(dbPath);

            try
            {
                using var cnx = new SqliteConnection($"Data Source={dbPath}");
                cnx.Open();

                // 1) LISTADO DE TABLAS.

                var tables = SchemaExplorer.GetTables(cnx);

                if (tables.Length == 0)
                {
                    Console.WriteLine($"\nNo hay tablas en {dbName}.");
                    ConsolePause.Wait();
                    return;
                }

                Console.WriteLine($"\nTablas en {dbName}.");

                for (int i = 0; i < tables.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {tables[i]}");
                }

                // 2) SELECCIÓN DE TABLA.

                Console.WriteLine("\nSeleccione la tabla para modificar: ");

                int index = DbBrowser.ReadIndexOrEscape(tables.Length);
                if (index == -1) return;
                string table = tables[index];

                // 3) MOSTRAR COLUMNAS ACTUALES.

                var cols = SchemaExplorer.GetColumns(cnx, table);

                if (cols.Length == 0)
                {
                    Console.WriteLine($"\nSin columnas en '{table}'");
                    Console.WriteLine("\nSeleccione una opción: ");
                    Console.WriteLine("1 - Crear columna.");
                    Console.WriteLine("ESC - Salir.");

                    var key00 = Console.ReadKey(intercept: true);

                    if (key00.Key == ConsoleKey.D1 || key00.Key == ConsoleKey.NumPad1)
                    {
                        CreateColumn.Execute(dbPath, table);
                    }
                    return;
                }

                else
                {
                    Console.WriteLine($"\nColumnas de la tabla de '{table}'");
                    foreach (var c in cols)
                    {
                        Console.WriteLine($"      {c}");
                    }

                    Console.WriteLine("\nSeleccione una opción: ");
                    Console.WriteLine("1 - Crear columna.");
                    Console.WriteLine("2 - Modificar columna.");
                    Console.WriteLine("3 - Eliminar columna.");
                    Console.WriteLine("ESC - Cancelar.");

                    var key01 = Console.ReadKey(intercept: true);

                    switch (key01.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("Crear columna.");
                            CreateColumn.Execute(dbPath, table);
                            return;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Modificar columna.");
                          //  ModifyColumn.Execute(dbPath);
                            return;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("Eliminar columna.");
                            DeleteColumn.Execute(dbPath, table);
                            return;

                        case ConsoleKey.Escape:
                            return;

                        default:
                            Console.WriteLine("Opción incorrecta.");
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al modificar la tabla: {ex.Message}");
            }
}
    }
}
