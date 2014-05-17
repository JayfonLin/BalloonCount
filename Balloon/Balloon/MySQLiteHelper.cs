using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balloon
{
    public class MySQLiteHelper
    {
        static public SQLiteConnection CreateSQLiteConnection()
        {
            var dbPath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Activity.db");
            return new SQLite.SQLiteConnection(dbPath);
        }

        static public void createDB()
        {
            using (SQLiteConnection db = CreateSQLiteConnection())
            {
                db.CreateTable<ActivityInfo>();
            }
        }
    }
}
