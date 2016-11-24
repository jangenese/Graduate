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
        public SchoolYearViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
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
            schoolYearView.grade = getGradeFromChildren(sy.Id.ToString()).ToString();
            schoolYearView.parentLabel = "";
            schoolYearView.status = getStatus(sy.Id.ToString());
            return schoolYearView;
        }

        private int getCreditsFromChildren(String fid)
        {
            int i = 0;

            IList<Class> children = getChildrensChildren(fid);

            foreach (Class c in children)
            {
                i += c.credits;
            }

            return i;
        }

        private double getGradeFromChildren(String fid)
        {
            double grade = 0;
            int count = 0;

            IList<Class> children = getChildrensChildren(fid);

            foreach (Class c in children)
            {
                grade += c.grade;
                count++;
            }


            return Math.Round((grade / count), 2);
        }

       

        private String getStatus(String fid)
        {
            String status = "Completed";


            IList<Class> children = getChildrensChildren(fid);

            foreach (Class c in children)
            {
                if (!c.completed)
                {
                    status = "InProgress";
                }
            }


            return status;
        }
    }
}
