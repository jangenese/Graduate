using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;


namespace Graduate.Droid.ListAdapters
{
    /// <summary>
    /// Adapter that presents SchoolYears in a row-view
    /// </summary>
    public class SchoolYearListAdapter : BaseAdapter<SchoolYear>
    {
        Activity context = null;
        IList<SchoolYear> schoolYears = new List<SchoolYear>();

        public SchoolYearListAdapter(Activity context, IList<SchoolYear> schoolYears) : base()
        {
            this.context = context;
            this.schoolYears = schoolYears;
        }

        public override SchoolYear this[int position]
        {
            get { return schoolYears[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return schoolYears.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            
            var item = schoolYears[position];

            SchoolYearView sy = GraduateApp.Current.planner.getSchoolYear(item.Id.ToString());

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


            
            entry.Text = sy.label;
            
            statusEntry.Text = sy.status;
         
            gradeEntry.Text = sy.percentGrade;

    

            return row;
        }
    }
}