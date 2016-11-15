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
    public class ClassFragment : Fragment
    {
        protected ListView listView;

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

        public override void OnActivityCreated(Bundle savedInstanceState)
        {

           base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();

            classs = planner.getAllClasss();


            listView.Adapter = new Graduate.Droid.ListAdapters.ClassListAdapter(this.Activity, classs);
            
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
            var entity = classs[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(EntityDetail));
            intent.PutExtra("type", 3);
            intent.PutExtra("selectedEntityID", entity.Id);

            StartActivityForResult(intent, 100);
        }

    }
}