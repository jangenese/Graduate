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

using Graduate.Core;
using Graduate.Core.Data.Models;

namespace Graduate.Droid.Fragments
{
    public class SemesterFragment : Fragment
    {
        protected ListView listView;
        
        protected Planner planner;
        protected IList<Semester> semesters;

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

        public override void OnActivityCreated(Bundle savedInstanceState)
        {

            Console.WriteLine("Hello World On Activity Create");

            base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();

            semesters = planner.getAllSemesters();
           

            listView.Adapter = new Graduate.Droid.ListAdapters.SemesterListAdapter(this.Activity, semesters);               
               
            
        }



        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }
        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewGraduateEntities);
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