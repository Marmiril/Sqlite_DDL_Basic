using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite_DB_Manager;
using Microsoft.Data.Sqlite;


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; 
        Console.WriteLine("Gestor de BBDD SQLite iniciado");
        new MainCast().Run();
    }
}