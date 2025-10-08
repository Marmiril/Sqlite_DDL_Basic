using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLite_DB_Manager.Helpers;

namespace SQLite_DB_Manager.DDL
{
    /// <summary>
    /// Crea una tabla en la BBDD seleccionada por el usuario.
    /// </summary>

    internal static class CreateTable
    {
        public static void Execute()
        {
            // 1) Elegir BBDD.
            var files = DbBrowser.ListAndPrint();
            if (files.Length == 0) return;

            Console.WriteLine("Seleccione la BBDD para añadir tablas:");

            int index = DbBrowser.ReadIndexOrEscape(files.Length);
            if (index == null) return;

            string dbPath = files[index];
            Execute(dbPath);
        }

        public static void Execute(string dbPath)
        {
            string dbName = Path.GetFileName(dbPath);

            // 1) Pedir nombre de la tabla.
            string? tableName = ConsoleInput.ReadLineOrEscape("Indique el nombre de la nueva tabla: ");
            if (tableName == null) return;

            // 2) Tabla con una columna de ID.
            try
            {
                using var cnx = new SqliteConnection($"Data Source={dbPath};");
                cnx.Open();

                using var cmd = cnx.CreateCommand();
                cmd.CommandText = $"CREATE TABLE \"{tableName}\" (id INTEGER PRIMARY KEY AUTOINCREMENT);";
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Tabla {tableName} creada con éxito.");
                ConsolePause.Wait();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ConsolePause.Wait();
            }
        }
    }
}
