using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using RaysHotDogs.Core;

using Graduate.Core;
//using Graduate.Core.Models;
using Graduate.Core.Data.Models;

namespace Graduate.Droid.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        //protected HotDogDataService hotDogDataService;
        protected Planner planner;
        protected IList<Semester> graduateEntities;

        public BaseFragment()
        {
            // hotDogDataService = new HotDogDataService();
            planner = Graduate.Droid.GraduateApp.Current.planner;
            
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
           var entity = graduateEntities[e.Position];

           var intent = new Intent();
           intent.SetClass(this.Activity, typeof(EntityDetail));
           intent.PutExtra("selectedEntityID", entity.Id );

            StartActivityForResult(intent, 100);
        }

    }
}