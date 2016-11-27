using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using Graduate.Core;
using Graduate.Core.Data.Models;
using Graduate.Droid.ListAdapters;

namespace Graduate.Droid.Fragments
{
    public class SchoolYearFragment : Fragment
    {
        protected ListView listView;
        protected Planner planner;
        protected IList<SchoolYear> schoolYears;
        protected FloatingActionButton fab;

        protected TextView label;
        protected TextView status;
        protected TextView grade;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);         

            planner = GraduateApp.Current.planner;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.SchoolYearFragment, container, false);

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);


            if (resultCode == Result.Ok)
            {
                Console.WriteLine("Hello World Result Code OK");
                populateListView();
            }

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            label.Text = "SchoolYear";
            HandleEvents();
            populateListView();
            
        }

        private void populateListView() {

            schoolYears = planner.getAllSchoolYears();

            SchoolYearListAdapter syAdapter = new SchoolYearListAdapter(this.Activity, schoolYears);

            listView.Adapter = syAdapter;
        }



        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {


            //   var intent = new Intent(this.Activity, typeof(GraduateEntityEntryActivity));
            //   intent.PutExtra("type", 1);
            //   StartActivityForResult(intent, 1);


            showEntryForm();

        }


        private void showEntryForm() {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            //Remove fragment else it will crash as it is already added to backstack
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }

            ft.AddToBackStack(null);

            // Create and show the dialog.
            NewEntryDialogFragment dialogFrag = NewEntryDialogFragment.NewInstance(null);
            dialogFrag.SetTargetFragment(this, 1);
            dialogFrag.type = 1;
            dialogFrag.Show(ft, "dialog");
        }
        protected void FindViews()
        {
            fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab);
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewGraduateEntities);
            label = this.View.FindViewById<TextView>(Resource.Id.textViewLabel);
            status = this.View.FindViewById<TextView>(Resource.Id.textViewStatus);
            grade = this.View.FindViewById<TextView>(Resource.Id.textViewGrade);
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var entity = schoolYears[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(EntityDetail));
            intent.PutExtra("type", 1);
            intent.PutExtra("selectedEntityID", entity.Id);

            StartActivityForResult(intent, 100);
        }

    }
}