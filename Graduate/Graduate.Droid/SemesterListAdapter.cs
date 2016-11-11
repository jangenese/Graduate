using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Graduate.Core.Models;

namespace Graduate.Core
{
    /// <summary>
    /// Adapter that presents Tasks in a row-view
    /// </summary>
    public class SemesterListAdapter : BaseAdapter<Semester>
    {
        Activity context = null;
        IList<Semester> tasks = new List<Semester>();

        public SemesterListAdapter(Activity context, IList<Semester> tasks) : base()
        {
            this.context = context;
            this.tasks = tasks;
        }

        public override Semester this[int position]
        {
            get { return tasks[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return tasks.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            // Get our object for position
            var item = tasks[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            // will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()

            //			var view = (convertView ?? 
            //					context.LayoutInflater.Inflate(
            //					Resource.Layout.TaskListItem, 
            //					parent, 
            //					false)) as LinearLayout;
            //			// Find references to each subview in the list item's view
            //			var txtName = view.FindViewById<TextView>(Resource.Id.NameText);
            //			var txtDescription = view.FindViewById<TextView>(Resource.Id.NotesText);
            //			//Assign item's values to the various subviews
            //			txtName.SetText (item.Name, TextView.BufferType.Normal);
            //			txtDescription.SetText (item.Notes, TextView.BufferType.Normal);

            // TODO: use this code to populate the row, and remove the above view
            var view = (convertView ??
                context.LayoutInflater.Inflate(
                    Android.Resource.Layout.SimpleListItemChecked,
                    parent,
                    false)) as CheckedTextView;
            view.SetText(item.Label== "" ? "<new semester>" : item.Label, TextView.BufferType.Normal);
           // view.Checked = item.Done;


            //Finally return the view
            return view;
        }
    }
}