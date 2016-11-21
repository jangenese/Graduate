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

using Graduate.Core;
using Graduate.Core.Data.Models;
using Graduate.Droid.ListAdapters;
using Graduate.Core.MiscTools;
using Graduate.Core.View.Model;



namespace Graduate.Droid
{
    [Activity(Label = "EntityDetail")]
    public class EntityDetail : Activity
    {
        private TextView label;
        private int selectedID;
        private Planner planner;
        private ListView childrenList;
        private TextView name;
        private GraduateEntityBase entity = null;
        private int childrenPosition;

       
        


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EntityDetail);

            planner = GraduateApp.Current.planner;

            selectedID = Intent.Extras.GetInt("selectedEntityID");

            findViews();
            handleEvents();

            label.Text = selectedID.ToString();

            int enityType = Intent.Extras.GetInt("type");
            switch (enityType)
            {
                case 1:
                    Console.WriteLine("SchoolYear Recieved");
                    populateSchoolYearDetail(selectedID);
                    break;
                case 2:
                    Console.WriteLine("Semester Recieved");
                    populateSemesterDetail(selectedID);
                    break;
                case 3:
                    Console.WriteLine("Class Recieved");
                    populateClassDetail(selectedID);
                    break;
                default:
                    Console.WriteLine("Unknown Recieved");
                    break;
            }
        }

        private void findViews() {
            label = FindViewById<TextView>(Resource.Id.textViewOtherStuff);
            name = FindViewById<TextView>(Resource.Id.textViewName);
            childrenList = FindViewById<ListView>(Resource.Id.listViewChildItems);
        }

        private void handleEvents() {
            childrenList.ItemClick += ChildrenList_ItemClick;
        }

        private void ChildrenList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //  var entity = classs[e.Position];

            childrenPosition = e.Position;

            var intent = new Intent();
            intent.SetClass(this, typeof(EntityDetail));
            intent.PutExtra("type", 3);
            intent.PutExtra("selectedEntityID", entity.Id);

            StartActivityForResult(intent, 100);
        }

        private void populateSemesterDetail(int id) {
            Console.WriteLine("Looking for the semester with ID" +  id);

            SemesterView semester = planner.getSemester("1");

            name.Text = semester.label;

            IList<Class> children = semester.children;
            

            ClassListAdapter childAdapter = new ClassListAdapter(this, children);

            childrenList.Adapter = childAdapter;

            entity = children[childrenPosition];

        }

        private void populateSchoolYearDetail(int id) {
            SchoolYearView sy = planner.getSchoolYear("1");

            name.Text = sy.label;


            IList<Semester> children = sy.children;

            Graduate.Droid.ListAdapters.SemesterListAdapter childAdapter = new ListAdapters.SemesterListAdapter(this, children);

            childrenList.Adapter = childAdapter;

            entity = children[childrenPosition];
        }

        private void populateClassDetail(int id)
        {

            ClassView c = planner.getClass("1");

            name.Text = c.label;


            

        }







    }
}