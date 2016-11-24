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
        public SemesterViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
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
            SemesterView semesterView = new SemesterView();
            semesterView.id = sem.Id;
            semesterView.label = sem.label;
            semesterView.children = getChildren(sem.Id.ToString());
            semesterView.credits = getCreditsFromChildren(sem.Id.ToString()).ToString() ;
            semesterView.grade = getGradeFromChildren(sem.Id.ToString()).ToString();
            semesterView.parentLabel = getParentLabel(sem.Id.ToString());
            semesterView.status = getStatus(sem.Id.ToString());
            return semesterView;
        }

        private int getCreditsFromChildren(String fid)
        {
            int i = 0;

            IList<Class> children = getChildren(fid);

            foreach (Class c in children) {
                i += c.credits;
            }

            return i;
        }

        private double getGradeFromChildren(String fid)
        {
            double grade = 0;
            int count = 0;

            IList<Class> children = getChildren(fid);

            foreach (Class c in children)
            {
                grade += c.grade;
                count++;
            }


            return Math.Round((grade/count), 2);
        }

        private String getParentLabel(String fid) {
            SchoolYear sy = schoolYearManager.getSchoolYearByID(fid);
            return sy.label;
        }

        private String getStatus(String fid) {
            String status = "Completed";
           

            IList<Class> children = getChildren(fid);

            foreach (Class c in children)
            {
                if (!c.completed) {
                    status = "InProgress";
                }                
            }
            return status;
        }
    }
}
