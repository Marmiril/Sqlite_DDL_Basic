using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SQLite_DB_Manager.Helpers
{
    internal static class SchemaExplorer
    {
        public static string[] GetColumns(SqliteConnection cnx, string table)
        {
            HiddenColumns.Ensure(cnx);
            var hidden = HiddenColumns.GetHidden(cnx, table);

            using var cmd = cnx.CreateCommand();

            cmd.CommandText = $"PRAGMA table_info(\"{table.Replace("\"", "\"\"")}\");";

            using var rd = cmd.ExecuteReader();

            var cols = new List<string>();

            int nameIdx = rd.GetOrdinal("name");
            while (rd.Read())
            {
                var name = rd.GetString(nameIdx);
                if(!hidden.Contains(name)) cols.Add(name);
                // PRAGMA table_info: 0-cid, 1-name, 2-type, 3-notnull, 4-dflt_value, 5-spk.
                //cols.Add(rd.GetString(1)); // name.
            }
            return cols.ToArray();
        }

        public static string[] GetTables(SqliteConnection cnx)
        {
            if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

            using (var cmd = cnx.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT name
                    FROM sqlite_master
                    WHERE type = 'table'
                      AND name NOT LIKE 'sqlite_%'
                      AND name NOT LIKE '_meta_%'
                    ORDER BY name;";

                using var rd = cmd.ExecuteReader();

                var tables = new List<string>();

                while (rd.Read()) tables.Add(rd.GetString(0));

                return tables.ToArray();
            }

            using (var cmd2 = cnx.CreateCommand())
            {
                cmd2.CommandText = "PRAGMA table_list;";

                using var rd2 = cmd2.ExecuteReader();

                var tables2 = new List<string>();

                int idxSchema = rd2.GetOrdinal("schema");
                int idxName   = rd2.GetOrdinal("name");

                while (rd2.Read())
                {
                    var name = rd2.GetString(idxName);

                    if (!name.StartsWith("sqlite_", StringComparison.OrdinalIgnoreCase))
                    {
                        tables2.Add(name);
                    }                    
                }
                return tables2.ToArray();
            }            
        }
    }
}
