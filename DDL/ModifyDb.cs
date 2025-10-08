using Microsoft.Data.Sqlite;
using SQLite_DB_Manager.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DB_Manager.DDL
{
    internal static class ModifyDb
    {
        public static void Execute()
        {
            // 1) SELECCIÓN DE BBDD.

            var files = DbBrowser.ListAndPrint();

            if (files.Length == 0)
            { Console.WriteLine("No hay BBDD para modificar...");
              ConsolePause.Wait();
              return;
            }
            
            Console.WriteLine("Seleccione la BBDD para modificar: ");

            int index = DbBrowser.ReadIndexOrEscape(files.Length);

            if (index == -1) return; 

            string dbPath = files[index];
            string dbName = Path.GetFileName(dbPath);

            try
            {
                Console.WriteLine($"[DEBUG] Abriendo: {dbPath}");
                using var cn = new SqliteConnection($"Data Source={dbPath}");
                cn.Open();

                Console.WriteLine($"TABLAS en '{dbName}': ");

                var tables = SchemaExplorer.GetTables(cn);

                int count = 0;                                            
                
                foreach (var t in tables)
                {
                    count++;                   
                    Console.WriteLine($"{count} - {t}");

                    var cols = SchemaExplorer.GetColumns(cn, t);

                    if (cols.Length == 0) Console.WriteLine("      (sin columnas)");

                    else foreach (var c in cols) Console.WriteLine($"      . {c}");
                }

               // ConsolePause.Wait();
                if (count == 0)
                {
                    Console.WriteLine("Seleccione una opción: ");
                    Console.WriteLine("1 - Crear TABLA.");
                    Console.WriteLine("ESC - Salir.");

                    while (true)
                    {
                        var key0 = Console.ReadKey(intercept: true);

                        if (key0.Key == ConsoleKey.Escape) return;

                        if (key0.Key == ConsoleKey.D1 || key0.Key == ConsoleKey.NumPad1)
                        {
                            CreateTable.Execute(dbPath);
                            return;
                        }

                        Console.WriteLine("Opción no válida. Inténtelo de nuevo."); continue;
                    }
                }

                else
                {
                    Console.WriteLine("Seleccione una opción; ");
                    Console.WriteLine("1 - Crear TABLA.");
                    Console.WriteLine("2 - Modificar TABLA.");
                    Console.WriteLine("3 - Eliminar TABLA.");
                    Console.WriteLine("ESC - Salir.");

                    var key = Console.ReadKey(intercept: true);
                    Console.WriteLine();

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            CreateTable.Execute(dbPath);
                            return;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            ModifyTable.Execute(dbPath);
                            return;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            DeleteTable.Execute(dbPath);
                            return;
                    }                
                }   
               
            } catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

    }
}
