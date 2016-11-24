using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;
using SQLite;

namespace Graduate.Core.Repository
{
    public class ActivityRepository
    {
        private ActivityDataAccess dataAccess;
        public ActivityRepository(SQLiteConnection conn)
        {
            dataAccess = new ActivityDataAccess(conn);
        }

        public void saveItem(Activity c)
        {
            dataAccess.SaveItem<Activity>(c);
        }

        public Activity getItem(int id)
        {
            return dataAccess.getItemById(id);
        }

        public IEnumerable<Activity> getItems()
        {
            return dataAccess.GetItems<Activity>();
        }

        public IEnumerable<Activity> getItemsByFID(int fid)
        {
            return dataAccess.getItemsByFID(fid);
        }

    }
}
