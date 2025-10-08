using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace SQLite_DB_Manager.Helpers
{
    internal static class HiddenColumns
    {
        public static void Ensure(SqliteConnection cnx)
        {
            // CREA LA TABLA DE METADATOS SI NO EXISTE.

            using var cmd = cnx.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS _meta_hidden_columns (
                    table_name TEXT NOT NULL,
                    column_name TEXT NOT NULL,
                    PRIMARY KEY (table_name, column_name)
                );";

            cmd.ExecuteNonQuery();
        }

        // MARCA UNA COLUMNA COMO OCULTA.
        public static void Hide(SqliteConnection cnx, string table, string column)
        {
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO _meta_hidden_columns (table_name, column_name) VALUES ($t, $c);";
            cmd.Parameters.AddWithValue("$t", table);
            cmd.Parameters.AddWithValue("$c", column);
            cmd.ExecuteNonQuery();
        }

        // DESMARCA UNA COLUMNA Y VUELVE A SER VISIBLE.

        public static void Unhide(SqliteConnection cnx, string table, string column)
        {
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "DELETE FROM _meta_hidden_columns WHERE table_name = $t AND column_name = $c;";
            cmd.Parameters.AddWithValue("$t", table);
            cmd.Parameters.AddWithValue("$c", column);
            cmd.ExecuteNonQuery();
        }

        // DEVOLUCIÓN DEL SET COLUMNAS OCULTAS PARA UNA TABLA.
        
        public static HashSet<string> GetHidden(SqliteConnection cnx, string table)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT column_name FROM _meta_hidden_columns WHERE table_name = $t;";
            cmd.Parameters.AddWithValue("$t", table);

            using var rd = cmd.ExecuteReader();

            while(rd.Read()) set.Add(rd.GetString(0));
            return set;
        }
    }
}
