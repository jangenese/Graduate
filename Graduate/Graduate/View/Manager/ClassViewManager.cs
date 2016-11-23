using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;
using Graduate.Core.Manager;
using SQLite;

namespace Graduate.Core.View.Manager
{
   public class ClassViewManager
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        public ClassViewManager(SQLiteConnection conn)
        {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
        }

        public ClassView getClassView(String id)
        {
            Class c = classManager.getClassByID(id);

            return populateClassView(c);
        }

        private IList<Class> getChildren(String fid)
        {
            return classManager.getClasssByFID(fid).ToList<Class>();
        }

        private ClassView populateClassView(Class c)
        {
            ClassView classView = new ClassView();
            classView.id = c.Id;
            classView.label = c.label;
            classView.credits = c.credits.ToString();
            classView.grade = Math.Round(c.grade, 2).ToString();
            classView.parentLabel = getParentLabel(c.FId.ToString());
            classView.status = getStatus(c.completed);
            return classView;
        }

        private int getCreditsFromChildren(String fid)
        {
            int i = 0;

            IList<Class> children = getChildren(fid);

            foreach (Class c in children)
            {
                i += c.credits;
            }

            return i;
        }

        private String getParentLabel(String fid)
        {
            Semester sem = semesterManager.getSemesterByID(fid);
            return sem.label;
        }

        private String getStatus(Boolean b)
        {
            String status = "Completed";
                       
                if (!b)
                {
                    status = "InProgress";
                }  
            return status;
        }

    }
}
