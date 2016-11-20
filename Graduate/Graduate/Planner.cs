using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.Data.Models;
using Graduate.Core.Manager;
using Graduate.Core.MiscTools;

namespace Graduate.Core
{
 public   class Planner
    {
        SemesterManager semesterManager;
        ClassManager classManager;
        SchoolYearManager schoolYearManager;
        public Planner(SQLiteConnection conn) {
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
            schoolYearManager = new SchoolYearManager(conn);
         // makeContents();

            
        }

        public IList<Semester> getAllSemesters() {
            return semesterManager.getAll().ToList<Semester>();
        }




        private void makeContents() {
           semesterManager.addItem(new Semester("Winter 2015"));
           semesterManager.addItem(new Semester("Winter 2016"));
           semesterManager.addItem(new Semester("Winter 2017"));
           semesterManager.addItem(new Semester("Winter 2018"));
           semesterManager.addItem(new Semester("Winter 2019"));

            classManager.addItem(new Class("COMP 0011"));
            classManager.addItem(new Class("COMP 0012"));
            classManager.addItem(new Class("COMP 0013"));
            classManager.addItem(new Class("COMP 0014"));
            classManager.addItem(new Class("COMP 0015"));

            schoolYearManager.addItem(new SchoolYear("2014-2015"));
            schoolYearManager.addItem(new SchoolYear("2015-2016"));
            schoolYearManager.addItem(new SchoolYear("2016-2017"));
            schoolYearManager.addItem(new SchoolYear("2017-2018"));
            schoolYearManager.addItem(new SchoolYear("2018-2019"));
        }

        public String ToStringSemesters() {
            return semesterManager.toStringAllSemesters();
        }

        public Semester getSemester(int id) {
            return semesterManager.getSemester(id);
        }

        public IList<Class> getAllClasss()
        {
            return classManager.getAll().ToList<Class>();
        }

        public IList<SchoolYear> getAllSchoolYears()
        {
            return schoolYearManager.getAll().ToList<SchoolYear>();
        }


        public Class getClass(int id)
        {
            return classManager.getClass(id);
        }

        public SchoolYear getSchoolYear(int id)
        {
            return schoolYearManager.getSchoolYear(id);
        }

        public void saveSemester(Semester sem) {
            semesterManager.saveSemester(sem);
        }

        public void saveSchoolYear(SchoolYear sy) {
            schoolYearManager.addItem(sy);
        }

        public void saveClass(Class c) {
            classManager.addItem(c);
        }

        public IEnumerable<Class> getSemesterChildren(int fid) {
           return classManager.getClassesByFID(fid);
        }

        public IEnumerable<Semester> getSchoolYearChildren(int fid) {
            return semesterManager.getSemestersByFID(fid);
        }
       
    }
}
