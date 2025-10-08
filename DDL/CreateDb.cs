using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using SQLite_DB_Manager.Helpers;
using SQLite_DB_Manager.DDL;


namespace SQLite_DB_Manager.DDL
{
    public static class CreateDb
    {
        public static void Execute()
        {                     
            while (true)
            {
                string? raw = ConsoleInput.ReadLineOrEscape("Indique el nombre para la BBDD: ");
                if (raw == null) return;

                string fileName = Path.GetFileNameWithoutExtension(raw) + ".db";
                string fullPath = Storage.PathFor(fileName);


                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"{fileName} ya existe. Intente otro nombre:");
                    ConsolePause.Wait();
                    continue;
                }

                using var cn = new SqliteConnection($"Data Source={fullPath};Mode=ReadWriteCreate");
                cn.Open();

                Console.WriteLine($"Base de datos {fileName} creada con éxito.");

                // Opción de crear una primera tabla.
                bool? confirm = ConsoleInput.Confirmation("Desea crear su primera tabla");
                if(confirm == true) { CreateTable.Execute(fullPath); }

                ConsolePause.Wait();
                return;
            }
        }
    }
}
