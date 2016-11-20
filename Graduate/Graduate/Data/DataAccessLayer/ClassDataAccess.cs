using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class ClassDataAccess : GraduateDatabase
    {

        private static object collisionLock = new object();
        SQLiteConnection database;
        public ClassDataAccess(SQLiteConnection conn) : base(conn) {
            database = conn;
        }



        public Class getItemById(int id)
        {


            lock (collisionLock)
            {
                return database.Table<Class>().
                  FirstOrDefault(c => c.Id == id);
            }


        }

        public IEnumerable<Class> getItemsByFID(int fid)
        {
            return (from i in database.Table<Class>() where i.FId == fid select i);
        }

    }
}
