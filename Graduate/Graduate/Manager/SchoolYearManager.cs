using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;

using SQLite;

namespace Graduate.Core.Manager
{
    public class SchoolYearManager
    {

        SchoolYearDataAccess schoolYears;
        public SchoolYearManager(SQLiteConnection conn)
        {
            schoolYears = new SchoolYearDataAccess(conn);
        }


        public IEnumerable<SchoolYear> getAll()
        {
            return schoolYears.GetItems<SchoolYear>();
        }

        public void addItem(SchoolYear sem)
        {
            schoolYears.SaveItem<SchoolYear>(sem);
        }

        public String toStringAllSchoolYears()
        {
            String str = "";

            IList<SchoolYear> sems = schoolYears.GetItems<SchoolYear>().ToList<SchoolYear>();
            foreach (SchoolYear sem in sems)
            {
                str += sem.ToString() + "\n";
            }

            return str;
        }

        public SchoolYear getSchoolYear(int id)
        {
             return schoolYears.getItemById(id);

         //   return schoolYears.GetItem<SchoolYear>(id); broken

        }
    }
}
