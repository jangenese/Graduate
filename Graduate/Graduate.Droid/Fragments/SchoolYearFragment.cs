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

namespace Graduate.Droid.Fragments
{
    public class SchoolYearFragment : Fragment
    {
        protected ListView listView;
        protected Planner planner;
        protected IList<SchoolYear> schoolYears;
        protected FloatingActionButton fab;

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

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();

            schoolYears = planner.getAllSchoolYears();

            listView.Adapter = new Graduate.Droid.ListAdapters.SchoolYearListAdapter(this.Activity, schoolYears);
        }



        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {


            var intent = new Intent(this.Activity, typeof(GraduateEntityEntryActivity));
            StartActivity(intent);
        }
        protected void FindViews()
        {
            fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab);
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewGraduateEntities);
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