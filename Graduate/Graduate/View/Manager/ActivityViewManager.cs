using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using Graduate.Core.Manager;
using Graduate.Core.View.Model;

namespace Graduate.Core.View.Manager
{
    public class ClassActivityViewManager
    {

        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        ClassActivityManager cActivityManager;
        public ClassActivityViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ClassManager classManager, ClassActivityManager cActivityManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
            this.cActivityManager = cActivityManager;
        }

        public ClassActivityView getClassActivityView(String id)
        {
            ClassActivity classActivity = cActivityManager.getClassActivityByID(id);

            return populateClassActivityView(classActivity);
        }

        private IList<ClassActivity> getChildren(String fid)
        {
            return cActivityManager.getClassActivitiesByFID(fid).ToList<ClassActivity>();
        }

        private ClassActivityView populateClassActivityView(ClassActivity activity)
        {
            ClassActivityView activityView = new ClassActivityView();
            activityView.id = activity.Id;
            activityView.label = activity.label;
            activityView.weight = activity.weight.ToString();
            activityView.grade = Math.Round(activity.grade, 2).ToString();            
            return activityView;
        }       

       

      
    }
}
