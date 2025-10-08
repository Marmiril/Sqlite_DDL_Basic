using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLite_DB_Manager.DDL;
using SQLite_DB_Manager.Helpers;

namespace SQLite_DB_Manager
{
    internal class MainCast
    {
        public void Run()
        {

            while (true)
            {
                int option = 0;
                Console.Clear();
                Console.WriteLine("Bienvenido al gestor de BBDD SQLite de la firma Cobalt.");
                Console.WriteLine("Indique qué acción desea realizar (ESC para salir): ");
                Console.WriteLine("1 - Consultar BBDD existentes.");
                Console.WriteLine("2 - Crear BBDD.");
                Console.WriteLine("3 - Eliminar BBDD");
                Console.WriteLine("4 - Modificar BBDD (añadir/eliminar tablas).");

                var key = Console.ReadKey(intercept: true);
                
                if (key.Key == ConsoleKey.Escape) return;
                
                if (char.IsDigit(key.KeyChar))
                {
                    option = key.KeyChar - '0';

                    if (option <= 0 || option > 7) break;                    
                }

                else { Console.WriteLine("Opción incorrecta. Inténtelo de nuevo:"); continue; }



                    switch (option)
                    {
                        case 1:
                            DbBrowser.ListAndPrint(); ConsolePause.Wait(); break;
                        case 2:
                            CreateDb.Execute(); break;
                        case 3:
                            DeleteDb.Execute(); break;
                        case 4: 
                            ModifyDb.Execute(); break;
                        case 5: break;
                        case 6: break;
                        default: break;
                    }
            }



        }
    }
}
