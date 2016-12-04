using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Manager;
using Graduate.Core.View.Model;


namespace Graduate.Droid.ListAdapters
{
    /// <summary>
    /// Adapter that presents ClassActivityes in a row-view
    /// </summary>
    public class ClassActivityListAdapter : BaseAdapter<ClassActivity>
    {
        Activity context = null;
        IList<ClassActivity> classActivities = new List<ClassActivity>();

        public ClassActivityListAdapter(Activity context, IList<ClassActivity> classActivities) : base()
        {
            this.context = context;
            this.classActivities = classActivities;
        }

        public override ClassActivity this[int position]
        {
            get { return classActivities[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return classActivities.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var item = classActivities[position];

            ClassActivityView cActivity = GraduateApp.Current.planner.getClassActivity(item.Id.ToString());

            var row = convertView;

            if (row == null)
            {
                row = this.context.LayoutInflater.Inflate(Resource.Layout.PlannerListViewRow, parent, false);
            }
            /*
            TextView label = row.FindViewById<TextView>(Resource.Id.textViewLabel);
            TextView entry = row.FindViewById<TextView>(Resource.Id.textViewEntry);
            TextView status = row.FindViewById<TextView>(Resource.Id.textViewStatus);
            TextView statusEntry = row.FindViewById<TextView>(Resource.Id.textViewStatusEntry);
            TextView grade = row.FindViewById<TextView>(Resource.Id.textViewGrade);
            TextView gradeEntry = row.FindViewById<TextView>(Resource.Id.textViewGradeEntry);
            */


            
            TextView entry = row.FindViewById<TextView>(Resource.Id.textViewLabel);
            
            TextView statusEntry = row.FindViewById<TextView>(Resource.Id.textViewStatus);
            
            TextView gradeEntry = row.FindViewById<TextView>(Resource.Id.textViewGrade);


           
            entry.Text = cActivity.label;
           
            statusEntry.Text = cActivity.weight;
           
            gradeEntry.Text = cActivity.percentGrade;

            return row;
        }
    }
}