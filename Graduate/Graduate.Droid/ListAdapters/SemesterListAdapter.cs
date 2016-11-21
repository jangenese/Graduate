using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Graduate.Core.Data.Models;


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

            var row = convertView;

            if (row == null)
            {
                row = this.context.LayoutInflater.Inflate(Resource.Layout.ListViewPlanner, parent, false);
            }

            TextView label = row.FindViewById<TextView>(Resource.Id.textViewLabel);
            TextView entry = row.FindViewById<TextView>(Resource.Id.textViewEntry);
            TextView status = row.FindViewById<TextView>(Resource.Id.textViewStatus);
            TextView statusEntry = row.FindViewById<TextView>(Resource.Id.textViewStatusEntry);
            TextView grade = row.FindViewById<TextView>(Resource.Id.textViewGrade);
            TextView gradeEntry = row.FindViewById<TextView>(Resource.Id.textViewGradeEntry);



            label.Text = "Semester";
            entry.Text = item.label;
            status.Text = "Status";
            statusEntry.Text = "Completed";
            grade.Text = "Grade";
          //  gradeEntry.Text = item.grade.ToString();

            return row;
        }
    }
}