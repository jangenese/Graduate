using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
   public class SchoolYearDataAccess : GraduateDatabase
    {

        private static object collisionLock = new object();
        SQLiteConnection database;

        public SchoolYearDataAccess(SQLiteConnection conn) : base(conn) {
            database = conn;
        }
        public SchoolYear getItemById(int id)
        {
            lock (collisionLock)
            {
                return database.Table<SchoolYear>().
                  FirstOrDefault(sy => sy.Id == id);
            }
        }
    }


}
