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
    public class ClassFragment : Fragment
    {
        protected ListView listView;
        protected FloatingActionButton fab;
        protected Planner planner;
        protected IList<Class> classs;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            planner = GraduateApp.Current.planner;


        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.ClassFragment, container, false);

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
            HandleEvents();

            populateListView();

        }

        private void populateListView() {
            classs = planner.getAllClasss();

            ClassListAdapter classAdapter = new ClassListAdapter(this.Activity, classs);

            listView.Adapter = classAdapter;         

           
        }






        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {

            //  var intent = new Intent(this.Activity, typeof(GraduateEntityEntryActivity));
            //      intent.PutExtra("type", 3);
            //    StartActivityForResult(intent, 1);

            

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

            dialogFrag.type = 3;
            dialogFrag.Show(ft, "dialog");


         
            

        }

        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewGraduateEntities);
            fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab);
                
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var entity = classs[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(EntityDetail));
            intent.PutExtra("type", 3);
            intent.PutExtra("selectedEntityID", entity.Id);

            StartActivityForResult(intent, 100);
        }

    }
}