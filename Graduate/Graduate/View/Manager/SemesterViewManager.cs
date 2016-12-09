using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.View.Model;
using SQLite;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;

namespace Graduate.Core.View.Manager
{
  public  class SemesterViewManager
    {     

        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;

        GradeManager gradeManager;
        public SemesterViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, GradeManager gradeManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;

            this.gradeManager = gradeManager;
        }

        public SemesterView getSemesterView(String id)
        {
            Semester sem = semesterManager.getSemesterByID(id);

            return populateSemesterView(sem);
        }

        private IList<Class> getChildren(String fid)
        {
            return classManager.getClasssByFID(fid).ToList<Class>();
        }

        private SemesterView populateSemesterView(Semester sem)
        {
            System.Diagnostics.Debug.WriteLine("Semester Populate Called");



            SemesterView semesterView = new SemesterView();
            semesterView.id = sem.Id;
            semesterView.label = sem.label;
            semesterView.children = getChildren(sem.Id.ToString());
            semesterView.credits = getCreditsFromChildren(sem.Id.ToString()).ToString() ;
            semesterView.gpaGrade = formatGPA(getGPAGradeFromChildren(sem.Id.ToString()));
            semesterView.percentGrade = getPercentGradeFromChildren(sem.Id.ToString()).ToString();
            semesterView.letterGrade = formatLetterGrade(getLetterFromSchema(getPercentGradeFromChildren(sem.Id.ToString())));
            semesterView.parentLabel = getParentLabel(sem.FId.ToString());
            semesterView.status = getStatus(sem.Id.ToString());
            semesterView.statusLong = getLongStatus(sem.Id.ToString());
            return semesterView;
        }

        private double getCreditsFromChildren(String fid)
        {
            double i = 0;

            IList<Class> children = getChildren(fid);

            foreach (Class c in children) {
                i += c.credits;
            }

            return i;
        }


        private double getGPAGradeFromChildren(String fid)
        {
            double grade = 0.00;
            double totalCredits = 0;

            double points = 0;

            IList<Class> children = getChildren(fid);


            if (children.Count > 0)
            {
                foreach (Class c in children)
                {                    

                    points += (c.gpaGrade * c.credits);
                    totalCredits += c.credits;
                }

                grade = Math.Round((points/totalCredits), 2);
            }


            return grade;
        }

        private String formatLetterGrade(String letter)
        {
            String str = letter;

            if (str.Length <= 1)
            {
                str += " ";
            }


            return str;

        }

        private String formatGPA(double gpa)
        {
            return String.Format("{0:0.00}", gpa);

        }

        private int getPercentGradeFromChildren(String fid)
        {

            double grade = 0;                       
            double totalCredits = 0;

            double points = 0;

            IList<Class> children = getChildren(fid);


            if (children.Count > 0)
            {
                foreach (Class c in children)
                {
                    points += (c.percentGrade * c.credits);
                    totalCredits += c.credits;
                }

                grade = points / totalCredits;
            }


            return Convert.ToInt32(grade);
        }


        private String getParentLabel(String fid) {
            SchoolYear sy = schoolYearManager.getSchoolYearByID(fid);
            return sy.label;
        }

        private String getStatus(String fid) {
            String status = "C";
           

            IList<Class> children = getChildren(fid);

            foreach (Class c in children)
            {
                if (!c.completed) {
                    status = "INP";
                }                
            }
            return status;
        }

        private String getLongStatus(String fid)
        {
            String status = "Complete";


            IList<Class> children = getChildren(fid);

            foreach (Class c in children)
            {
                if (!c.completed)
                {
                    status = "InProgress";
                }
            }
            return status;
        }

        private String getLetterFromSchema(int percent)
        {
            return gradeManager.getByPercent(percent.ToString()).Letter;
        }
    }
}
