using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite_DB_Manager.Helpers;
using Microsoft.Data.Sqlite;
using System.IO;

namespace SQLite_DB_Manager.DDL
{
    internal static class CreateColumn
    {
        public static void Execute()
        {
            // Selección de BBDD.
            var files = DbBrowser.ListAndPrint();

            Console.WriteLine("Selecciones la BBDD a tratar.");

            int dbIndex = DbBrowser.ReadIndexOrEscape(files.Length);
            if (dbIndex == -1) return;

            string dbPath = files[dbIndex];
            string dbName = Path.GetFileName(dbPath);

            using var cnx = new SqliteConnection($"Data Source={dbPath}");
            cnx.Open();

            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' " +
                "AND name NOT LIKE 'sqlite_%' ORDER BY NAME;";

            using var rd = cmd.ExecuteReader();

            var tables = new List<string>();

            while (rd.Read()) tables.Add(rd.GetString(0));

            if (tables.Count() == 0)
            {
                Console.WriteLine($"No hay tablas en la BBDD{dbName}."); 
                bool? createTable = ConsoleInput.Confirmation("¿Desea crear una nueva tabla ahora?");
                if (createTable == true)
                {
                    CreateTable.Execute(dbPath);
                    Execute();
                    return;
                }
                Console.WriteLine("Operación cancelada. No hay tablas disponibles.");
                ConsolePause.Wait();
                return;
            }

            Console.WriteLine($"Tablas en '{dbName}':");
            for(int i = 0; i < tables.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {tables[i]}");
            }

            Console.WriteLine("Seleccione la tabla a modificar.");
            int tableIndex = DbBrowser.ReadIndexOrEscape(tables.Count);
            if (tableIndex == -1) return;

            string table = tables[tableIndex];

            // Pedir nombre de la columna.
            string? columnName = ConsoleInput.ReadLineOrEscape("Indique el nombre de la nueva columna: ");
            if (columnName == null) return;

            string[] types = { "TEXT", "INTEGER", "REAL", "NUMERIC", "BLOB" };
            Console.WriteLine("Seleccione el tipo de datos de la columna: ");
            for (int i = 0; i < types.Length; i++) Console.WriteLine($"{i + 1} - {types[i]}");

            int typeIndex = DbBrowser.ReadIndexOrEscape(types.Length);
            if (typeIndex == -1) return;

            string selectedType = types[typeIndex];

            // Ejecución de ALTER TABLE ADD COLUMN.
            try
            {
                using var alter = cnx.CreateCommand();

                alter.CommandText = "ALTER TABLE '{table}' ADD COLUMN '{columnName}' {selectedType};";
                alter.ExecuteNonQuery();

                Console.WriteLine($"Columna '{columnName}' creada en la tabla '{table}'");
                ConsolePause.Wait();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ConsolePause.Wait();
            }

            Console.WriteLine($"Columna a crear '{columnName}'.");
            ConsolePause.Wait();















        }

        public static void Execute(string dbPath, string table)
        {
            string dbName = Path.GetFileName(dbPath);

            using var cnx = new SqliteConnection($"Data Source={dbPath}");
            cnx.Open();

            Console.Clear();
            Console.WriteLine($"\n === Adición de columna a '{table}' de {dbName} === \n");
            Console.WriteLine();

            // AÑADIR NOMBRE DE LA COLUMNA Y COMPROBAR QUE NO ESTÁ DUPLICADO.

            string? col = ConsoleInput.ReadLineOrEscape("Nombre de la nueva columna:");
            if (col == null) return;

            string colSafe = col.Trim();

            var existing = SchemaExplorer.GetColumns(cnx, table);
            
            if(existing.Any(c => string.Equals(c, colSafe, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"\nYa existe una columna con ese nombre.");
                ConsolePause.Wait();
                return;
            }


            // TIPOS DE DATOS BÁSICOS DE SQLITE.

            (string Label, string Sql)[] typeMenu =
                {
                ("TEXT",      "TEXT"),
                ("INTEGER","INTEGER"),
                ("REAL",      "REAL"), 
                ("NUMERIC","NUMERIC"),
                ("BLOB",      "BLOB"),
            };

            Console.WriteLine($"\nSeleccione el tipo de dato de la columna{col}: ");
            for (int i = 0; i < typeMenu.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {typeMenu[i].Label}");
            }

            int index = DbBrowser.ReadIndexOrEscape(typeMenu.Length);
            if (index == null) return;

            string type = typeMenu[index].Sql;

            // ALTER TALBE... ADD COLUMN.
            string tableQuoted = table.Replace("\"", "\"\"");
            string colQuoted = colSafe.Replace("\"", "\"\"");

            using var cmd = cnx.CreateCommand();
            cmd.CommandText = $"ALTER TABLE \"{tableQuoted}\" ADD COLUMN \"{colQuoted}\" {type};";

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"\nColumna '{colQuoted}' añadida correctamente a '{tableQuoted}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la columna '{colQuoted}': {ex.Message}");
            }
            ConsolePause.Wait();
        }
    }
}
