using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class ActivityDataAccess : GradeDataAccess
    {
        private static object collisionLock = new object();
        SQLiteConnection database;
        public ActivityDataAccess(SQLiteConnection conn): base(conn) {
            database = conn;
        }

        public Activity getItemById(int id)
        {

            lock (collisionLock)
            {
                return database.Table<Activity>().
                  FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Activity> getItemsByFID(int fid)
        {
            return (from i in database.Table<Activity>() where i.FId == fid select i);
        }
    }
}
