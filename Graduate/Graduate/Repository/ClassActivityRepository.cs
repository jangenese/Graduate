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
    public class ClassActivityRepository
    {
        private ClassActivityDataAccess dataAccess;
        public ClassActivityRepository(SQLiteConnection conn)
        {
            dataAccess = new ClassActivityDataAccess(conn);
        }

        public void saveItem(ClassActivity c)
        {
            dataAccess.SaveItem<ClassActivity>(c);
        }

        public ClassActivity getItem(int id)
        {
            return dataAccess.getItemById(id);
        }

        public IEnumerable<ClassActivity> getItems()
        {
            return dataAccess.GetItems<ClassActivity>();
        }

        public IEnumerable<ClassActivity> getItemsByFID(int fid)
        {
            return dataAccess.getItemsByFID(fid);
        }


        public void deleteItem(ClassActivity cActivity)
        {
            dataAccess.DeleteItem<ClassActivity>(cActivity);
        }
    }
}
