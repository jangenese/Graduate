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
        public SemesterViewManager(SQLiteConnection conn)
        {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
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
            semesterView.children = getChildren("1");
            semesterView.credits = getCreditsFromChildren("1").ToString() ;
            semesterView.grade = getGradeFromChildren("1").ToString();
            semesterView.parentLabel = getParentLabel("1");
            semesterView.status = getStatus("1");
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


            return grade/count;
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
