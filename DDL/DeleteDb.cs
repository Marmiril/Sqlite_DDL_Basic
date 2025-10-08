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
    public static class DeleteDb
    {
        public static void Execute()
        {
            var files = DbBrowser.ListAndPrint();
            if (files.Length == 0) return;

            Console.WriteLine("Indique la BBDD para eliminar (ESC para salir)...");
            int index = DbBrowser.ReadIndexOrEscape(files.Length);
            if (index == -1) return;

            string path = files[index];
            string name = Path.GetFileName(path);

            bool? ok = ConsoleInput.Confirmation($"Eliminar '{name}' definitivamente?");
            if (ok != true) return;

            try
            {
                /*
                SqliteConnection.ClearAllPools();
                GC.Collect(); GC.WaitForPendingFinalizers();
                */
                File.Delete(path);
                /*
                if (File.Exists(path + "-wal")) File.Delete(path + "-wal");
                if (File.Exists(path + "-shm")) File.Delete(path + "-shm");
                */
                Console.WriteLine($"BBDD '{name}' eliminada.");
                ConsolePause.Wait();
            }
            catch (IOException)
            {
                Console.WriteLine("No se pudo eliminar: el archivo está en uso (cierra cualquier visor/editor que la tenga abierta).");
                ConsolePause.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ConsolePause.Wait();
            }
        }

    }
}
