using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.DAL.TableGateways;

using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;
namespace Graduate.Core
{
   public class Planner
    {

        SemesterTableGateway semesters;
        SemesterDataAccess semesterDA;
        public Planner(SQLiteConnection conn) {
            semesters = new SemesterTableGateway(conn);
          

            semesterDA = new SemesterDataAccess(conn);

            populateSemester();
        }


        

        public IList<Semester> getAllSemesters() {
            //return new List<Semester>(semesters.GetItems());
            IEnumerable<Semester> enumerable = semesterDA.GetItems<Semester>();
            List<Semester> semesters = enumerable.ToList<Semester>();

            return semesters;
           
        }

        private void populateSemester() {
            semesterDA.SaveItem<Semester>(new Semester("Winter 2015"));
            semesterDA.SaveItem<Semester>(new Semester("Winter 2015"));
            semesterDA.SaveItem<Semester>(new Semester("Winter 2015"));
            semesterDA.SaveItem<Semester>(new Semester("Winter 2015"));
            semesterDA.SaveItem<Semester>(new Semester("Winter 2015"));
            
        }
    }
}
