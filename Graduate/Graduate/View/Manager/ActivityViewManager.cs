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
    public class ActivityViewManager
    {

        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ActivityManager classManager;
        public ActivityViewManager(SchoolYearManager schoolYearManager, SemesterManager semesterManager, ActivityManager classManager)
        {
            this.schoolYearManager = schoolYearManager;
            this.semesterManager = semesterManager;
            this.classManager = classManager;
        }

        public ActivityView getActivityView(String id)
        {
            Activity c = classManager.getActivityByID(id);

            return populateActivityView(c);
        }

        private IList<Activity> getChildren(String fid)
        {
            return classManager.getActivitysByFID(fid).ToList<Activity>();
        }

        private ActivityView populateActivityView(Activity activity)
        {
            ActivityView activityView = new ActivityView();
            activityView.id = activity.Id;
            activityView.label = activity.label;
            activityView.weight = activity.weight.ToString();
            activityView.grade = Math.Round(activity.grade, 2).ToString();            
            return activityView;
        }       

       

      
    }
}
