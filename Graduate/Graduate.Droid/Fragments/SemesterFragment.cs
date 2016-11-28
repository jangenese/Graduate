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
    public class SemesterFragment : Fragment
    {
        protected ListView listView;
        protected FloatingActionButton fab;
        protected Planner planner;
        protected IList<Semester> semesters;

        protected TextView label;
        protected TextView status;
        protected TextView grade;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


            planner = GraduateApp.Current.planner;

            Console.WriteLine("Hello World Semester Initiated");
            Console.WriteLine("Hello World Planner was passed");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.SemesterFragment, container, false);

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

            Console.WriteLine("Hello World On Activity Create");

            base.OnActivityCreated(savedInstanceState);

            FindViews();
            label.Text = "Semester";
            HandleEvents();

            populateListView();
            
        }

        private void populateListView(){
            semesters = planner.getAllSemesters();
            SemesterListAdapter semAdapter = new SemesterListAdapter(this.Activity, semesters);
            listView.Adapter = semAdapter;
        }

       
        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {




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
            dialogFrag.type = 2;
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
            var entity = semesters[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(EntityDetail));
            intent.PutExtra("type", 2);
            intent.PutExtra("selectedEntityID", entity.Id);

            StartActivityForResult(intent, 100);
        }

    }
}