using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using SQLite_DB_Manager.Helpers;

namespace SQLite_DB_Manager.DDL
{
    internal static class DeleteTable
    {
        public static void Execute(string dbPath)
        {
            string dbName = Path.GetFileName(dbPath);

            try
            {
                using var cn = new SqliteConnection($"Data Source={dbPath}");
                cn.Open();

                // 1) LISTADO DE TABLAS.
                
                var tables = SchemaExplorer.GetTables(cn);

                if (tables.Length == 0)
                {
                    Console.WriteLine($"No hay tablas en {dbName}.");
                    ConsolePause.Wait();
                    return;
                }

                Console.WriteLine($"Tablas en {dbName}: ");

                for (int i = 0;i < tables.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {tables[i]}");
                }

                // 2) SELECCIÓN DE TABLA PARA ELIMINAR.

                Console.WriteLine("\nSeleccione la tabla a eliminar: ");

                int index = DbBrowser.ReadIndexOrEscape(tables.Length);

                if (index == -1) return;

                string table = tables[index];

                // 3) CONFIRMACIÓN DE LA ELIMINACIÓN.

                bool? ok = ConsoleInput.Confirmation($"Seguro que desea elmininar la tabla de {table}");
                if (ok != true) return;

                // 4) DROP TABLE.
                using var cmd = cn.CreateCommand();

                string safe = table.Replace("\"", "\"\""); // Las comillas dobles.

                cmd.CommandText = $"DROP TABLE \"{safe}\";";

                cmd.ExecuteNonQuery();

                Console.WriteLine($"\nTabla '{safe}' eliminada con éxito.");
                ConsolePause.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la tabla: {ex.Message}");
                ConsolePause.Wait();
            }
        }
    }
}
