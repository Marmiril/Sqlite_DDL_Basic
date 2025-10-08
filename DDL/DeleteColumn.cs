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
    internal static class DeleteColumn
    {
        public static void Execute(string dbPath, string table)
        {
            string dbName = Path.GetFileName(dbPath);

            using var cnx = new SqliteConnection($"Data Source={dbPath}");
            cnx.Open();

            Console.Clear();
            Console.WriteLine($" === Eliminar columna en {table} de {dbName} === \n");

            // 1) LISTAR COLUMNAS VISIBLES.

            var cols = SchemaExplorer.GetColumns(cnx, table);
            if (cols.Length == 0)
            {
                Console.WriteLine($"No hay columnas en {table}.");
                ConsolePause.Wait();
                return;
            }

            for (int i = 0; i < cols.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {cols[i]}");
            }

            Console.WriteLine("Seleccione una columna para eliminar...");
            int index = DbBrowser.ReadIndexOrEscape(cols.Length);

            string colToHide = cols[index];

            bool? confirm = ConsoleInput.Confirmation("¿Seguro que desea eliminar esta columna?");
            if (confirm != true) return;

            HiddenColumns.Ensure(cnx);
            HiddenColumns.Hide(cnx, table, colToHide);

            Console.WriteLine($"Columna {colToHide} eliminada correctamente.");
            ConsolePause.Wait();





        }
    }
}
