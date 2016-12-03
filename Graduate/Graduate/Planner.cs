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
    public class Planner
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        ClassActivityManager classActivityManager;
        GradeManager gradeManager;

        SchoolYearViewManager schoolYearViewManager;
        SemesterViewManager semesterViewManager;
        ClassViewManager classViewManager;
        ClassActivityViewManager classActivityViewManager;

        public Planner(SQLiteConnection conn) {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
            classActivityManager = new ClassActivityManager(conn);
            gradeManager = new GradeManager(conn);

            schoolYearViewManager = new SchoolYearViewManager(schoolYearManager, semesterManager, classManager);
            semesterViewManager = new SemesterViewManager(schoolYearManager, semesterManager, classManager);
            classViewManager = new ClassViewManager(schoolYearManager, semesterManager, classManager, classActivityManager);
            classActivityViewManager = new ClassActivityViewManager(schoolYearManager, semesterManager, classManager, classActivityManager);
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

        public ClassActivityView getClassActivity(String id) {
            return classActivityViewManager.getClassActivityView(id);
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

        public void saveClassActivity(String fid, String label, String grade, String weight, Boolean completed)
        {
            classActivityManager.SaveItem(fid, label, grade, weight, completed);
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

        public IList<ClassActivity> getAllClassAcivities() {
            return classActivityManager.getClassActivities().ToList<ClassActivity>();
        }

        public List<String> getAllSchoolYearLabels() {
            return schoolYearManager.getSchoolYearLabels();
        }

        public List<String> getAllSemesterLabels() {
            return semesterManager.getSemesterLabels();
        }

        public List<String> getAllLetterGrades() {
            return gradeManager.getLetterGrades();
        }

        public List<String> getAllPercentGrades()
        {
            return gradeManager.getPercentGrades();
        }

        public List<String> getAllGPAGrades()
        {
            return gradeManager.getGPAs();
        }

        public void deleteClass(String id) {
            classManager.deleteClass(id);
        }

        public void deleteSemester(String id) {
            semesterManager.deleteSemester(id);
        }

        public void deleteSchoolYear(String id) {
            schoolYearManager.deleteSchoolYear(id);
        }

        public void deleteClassActivity(String id) {
            classActivityManager.deleteClassActivity(id);
        }

        public void editClass(String id, String fid, String label, String grade, String credits, Boolean completed) {
            classManager.UpdateItem(id, fid, label, grade, credits, completed);
        }

        public void editSemester(String id, String fid, String label) {
            semesterManager.UpdateItem(id, fid, label);
        }

        public void editSchoolYear(String id, String label) {
            schoolYearManager.UpdateItem(id, label);
        }

        public void editClassActivity(String id, String fid, String label, String grade, String weight, Boolean completed) {
            classActivityManager.UpdateItem(id, fid, label, grade, weight, completed);
        }
        
    }
}
