using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;

namespace Graduate.Core.View.Manager
{
    public class SchoolYearViewManager
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;

        GradeManager gradeManager;
        public SchoolYearViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, GradeManager gradeManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;

            this.gradeManager = gradeManager;
        }

        public SchoolYearView getSchoolYearView(String id)
        {
            SchoolYear sy = schoolYearManager.getSchoolYearByID(id);

            return populateSchoolYearView(sy);
        }

        private IList<Class> getChildrensChildren(String fid)
        {
            List<Class> classChildren = new List<Class>();
            IEnumerable<Semester> semesterChildren = semesterManager.getSemestersByFID(fid);

            foreach (Semester sem in semesterChildren) {
                IEnumerable<Class> children = classManager.getClasssByFID(sem.Id.ToString());
                foreach (Class c in children) {
                    classChildren.Add(c);
                }
            }

            return classChildren;
        }

        private IList<Semester> getChildren(String fid) {
            return semesterManager.getSemestersByFID(fid).ToList<Semester>();
        }

        private SchoolYearView populateSchoolYearView(SchoolYear sy)
        {
            SchoolYearView schoolYearView = new SchoolYearView();
            schoolYearView.id = sy.Id;
            schoolYearView.label = sy.label;
            schoolYearView.children = getChildren(sy.Id.ToString());
            schoolYearView.credits = getCreditsFromChildren(sy.Id.ToString()).ToString();
            schoolYearView.gpaGrade = getGPAGradeFromChildren(sy.Id.ToString()).ToString();
            schoolYearView.percentGrade = getPercentGradeFromChildren(sy.Id.ToString()).ToString();
            schoolYearView.letterGrade = getLetterFromSchema(getPercentGradeFromChildren(sy.Id.ToString()));        
            schoolYearView.parentLabel = "";
            schoolYearView.status = getStatus(sy.Id.ToString());
            return schoolYearView;
        }

        private int getCreditsFromChildren(String fid)
        {
            int i = 0;

            IList<Class> children = getChildrensChildren(fid);

            if (children.Count > 0) {
                foreach (Class c in children)
                {
                    i += c.credits;
                }
            }
                      

            return i;
        }

        private double getGPAGradeFromChildren(String fid)
        {
            double grade = 0;
            int count = 0;

            IList<Class> children = getChildrensChildren(fid);
            

            if (children.Count > 0)
            {
                foreach (Class c in children)
                {
                    grade += c.gpaGrade;
                    count++;
                }

                grade = Math.Round((grade / count), 2);
            }


            return grade;
        }

        private int getPercentGradeFromChildren(String fid) {

            int grade = 0;
            int count = 0;

            IList<Class> children = getChildrensChildren(fid);


            if (children.Count > 0)
            {
                foreach (Class c in children)
                {
                    grade += c.percentGrade;
                    count++;
                }
                grade = grade / count;
            }


            return grade;
        }
           
       

        private String getStatus(String fid)
        {
            String status = "C";


            IList<Class> children = getChildrensChildren(fid);

            foreach (Class c in children)
            {
                if (!c.completed)
                {
                    status = "INP";
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
