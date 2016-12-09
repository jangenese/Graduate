using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Repository
{
    public class ClassRepository
    {
        private ClassDataAccess dataAccess;
        public ClassRepository(SQLiteConnection conn) {
            dataAccess = new ClassDataAccess(conn);
        }

        public void saveItem(Class c) {
            dataAccess.SaveItem<Class>(c);
        }

        public Class getItem(int id) {
            return dataAccess.getItemById(id);
        }

        public IEnumerable<Class> getItems() {
            return dataAccess.GetItems<Class>();
        }

        public IEnumerable<Class> getItemsByFID(int fid) {
            return dataAccess.getItemsByFID(fid);
        }

        public void deleteItem(Class c) {
            dataAccess.DeleteItem<Class>(c);
        }

        public void deleteItem(int id)
        {
            dataAccess.DeleteClass(id);
        }

    }
}
