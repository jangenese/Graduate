using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.DAL.TableGateways;
using Graduate.Core.Models;

namespace Graduate.Core
{
   public class Planner
    {
        SemesterTableGateway semesters;
        public Planner(SQLiteConnection conn) {
            semesters = new SemesterTableGateway(conn);
            populateSemester();
        }


        public void addNewSemester(String label) {
            Semester sem = new Semester(label);

            semesters.SaveItem(sem);
        }

        public IList<Semester> getAllSemesters() {
            return new List<Semester>(semesters.GetItems());
        }

        private void populateSemester() {
            semesters.SaveItem(new Semester("Winter 2014"));
            semesters.SaveItem(new Semester("Winter 2015"));
            semesters.SaveItem(new Semester("Winter 2016"));
            semesters.SaveItem(new Semester("Winter 2017"));
            semesters.SaveItem(new Semester("Winter 2018"));

        }
    }
}
