using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;


namespace Graduate.Droid.ListAdapters
{
    /// <summary>
    /// Adapter that presents Semesters in a row-view
    /// </summary>
    public class SemesterListAdapter : BaseAdapter<Semester>
    {
        Activity context = null;
        IList<Semester> semesters = new List<Semester>();

        public SemesterListAdapter(Activity context, IList<Semester> semesters) : base()
        {
            this.context = context;
            this.semesters = semesters;
        }

        public override Semester this[int position]
        {
            get { return semesters[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return semesters.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var item = semesters[position];
            SemesterView sem = GraduateApp.Current.planner.getSemester(item.Id.ToString());

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


          
            entry.Text = sem.label;            
            statusEntry.Text = sem.status;          
            gradeEntry.Text = sem.grade;

            return row;
        }
    }
}