using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQLite_DB_Manager.Helpers
{
    internal static class Storage
    {
        private static readonly string Root = Path.Combine(
    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName,
    "Databases");



        public static string PathFor(string fileName)
        {
            EnsureRoot();
            return Path.Combine(Root, fileName);
        }

        private static void EnsureRoot()
        {
            if (!Directory.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }
        }
        
        public static IEnumerable<string> ListDataBases()
        {
            EnsureRoot();
            return Directory.EnumerateFiles(Root, "*.db")
                .Concat(Directory.EnumerateFiles(Root, "*.sqlite"))
                .OrderBy(Path.GetFileName);

        }
        
    }
}
