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
    public class SemesterRepository
    {
        private SemesterDataAccess dataAccess;
        public SemesterRepository(SQLiteConnection conn)
        {
            dataAccess = new SemesterDataAccess(conn);
        }

        public void saveItem(Semester sem)
        {
            dataAccess.SaveItem<Semester>(sem);
        }

        public Semester getItem(int id)
        {
            return dataAccess.getItemById(id);
        }

        public IEnumerable<Semester> getItems()
        {
            return dataAccess.GetItems<Semester>();
        }

        public IEnumerable<Semester> getItemsByFID(int fid)
        {
            return dataAccess.getItemsByFID(fid);
        }
    }
}
