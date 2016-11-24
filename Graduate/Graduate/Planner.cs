using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.View.Manager;
using Graduate.Core.View.Model;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;


namespace Graduate.Core
{
 public   class Planner
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        ActivityManager activityManager;

        SchoolYearViewManager schoolYearViewManager;
        SemesterViewManager semesterViewManager;
        ClassViewManager classViewManager;

        public Planner(SQLiteConnection conn) {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
            activityManager = new ActivityManager(conn);

            schoolYearViewManager = new SchoolYearViewManager(schoolYearManager, semesterManager, classManager);
            semesterViewManager = new SemesterViewManager(schoolYearManager, semesterManager, classManager);
            classViewManager = new ClassViewManager(schoolYearManager, semesterManager, classManager);
        }

        public SemesterView getSemester(String id) {
           return semesterViewManager.getSemesterView(id);

        }

        public ClassView getClass(String id) {
            return classViewManager.getClassView(id);
        }

        public SchoolYearView getSchoolYear(String id) {
            return schoolYearViewManager.getSchoolYearView(id);
        }

        public void saveSemester(String fid, String label) {
            semesterManager.SaveItem(fid, label);
        }

        public void saveSchoolYear(String label) {
            schoolYearManager.SaveItem(label);
        }

        public void saveClass(String fid, String label, String grade, String credits, Boolean completed) {
            classManager.SaveItem(fid, label, grade, credits, completed);
        }

        public void saveActivity(String fid, String label, String grade, String weight, Boolean completed)
        {
            activityManager.SaveItem(fid, label, grade, weight, completed);
        }

        public IList<SchoolYear> getAllSchoolYears()
        {
            return schoolYearManager.getSchoolYears().ToList<SchoolYear>();
        }

        public IList<Semester> getAllSemesters()
        {
            return semesterManager.getSemesters().ToList<Semester>();
        }

        public IList<Class> getAllClasses()
        {
            return classManager.getClasses().ToList<Class>();
        }

        public List<String> getAllSchoolYearLabels() {
           return schoolYearManager.getSchoolYearLabels();
        }

        public List<String> getAllSemesterLabels() {
            return semesterManager.getSemesterLabels();
        }

    }
}
