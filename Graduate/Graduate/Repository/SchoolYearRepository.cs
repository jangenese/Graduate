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
    public class SchoolYearRepository
    {
        private SchoolYearDataAccess dataAccess;
        public SchoolYearRepository(SQLiteConnection conn)
        {
            dataAccess = new SchoolYearDataAccess(conn);
        }

        public void saveItem(SchoolYear sy)
        {
            dataAccess.SaveItem<SchoolYear>(sy);
        }

        public SchoolYear getItem(int id)
        {
            return dataAccess.getItemById(id);
        }

        public IEnumerable<SchoolYear> getItems()
        {
            return dataAccess.GetItems<SchoolYear>();
        }        
    }
}
