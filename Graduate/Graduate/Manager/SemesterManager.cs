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
 public   class SemesterManager
    {

        SemesterDataAccess semesters;
        public SemesterManager(SQLiteConnection conn) {
            semesters = new SemesterDataAccess(conn);
        }


        public IEnumerable<Semester> getAll() {
            return semesters.GetItems<Semester>();
        }

        public void addItem(Semester sem) {
            semesters.SaveItem<Semester>(sem);
        }

        public String toStringAllSemesters() {
            String str = "";

            IList<Semester> sems = semesters.GetItems<Semester>().ToList<Semester>();
            foreach (Semester sem in sems) {
                str += sem.ToString() + "\n";
            }

            return str;
        }

        public Semester getSemester(int id) {
            return semesters.getItemById(id);

        }
    }
}
