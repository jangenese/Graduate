using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class ClassActivityDataAccess : GradeDataAccess
    {
        private static object collisionLock = new object();
        SQLiteConnection database;
        public ClassActivityDataAccess(SQLiteConnection conn): base(conn) {
            database = conn;
        }

        public ClassActivity getItemById(int id)
        {

            lock (collisionLock)
            {
                return database.Table<ClassActivity>().
                  FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<ClassActivity> getItemsByFID(int fid)
        {
            return (from i in database.Table<ClassActivity>() where i.FId == fid select i);
        }
    }
}
