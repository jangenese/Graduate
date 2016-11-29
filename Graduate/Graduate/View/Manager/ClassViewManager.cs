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
        ClassActivityManager cActivityManager;
        public ClassViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, ClassActivityManager cActivityManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
            this.cActivityManager = cActivityManager;
        }

        public ClassView getClassView(String id)
        {
            Class c = classManager.getClassByID(id);

            return populateClassView(c);
        }

        private IList<ClassActivity> getChildren(String fid)
        {
            return cActivityManager.getClassActivitiesByFID(fid).ToList<ClassActivity>();
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
            classView.children = getChildren(c.Id.ToString());
            return classView;
        }
               
        private String getParentLabel(String fid)
        {
            Semester sem = semesterManager.getSemesterByID(fid);
            return sem.label;
        }

        private String getStatus(Boolean b)
        {
            String status = "C";
                       
                if (!b)
                {
                    status = "INP";
                }  
            return status;
        }

    }
}
