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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EntityDetail);

            planner = GraduateApp.Current.planner;

            selectedID = Intent.Extras.GetInt("selectedEntityID");

            findViews();

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

        private void populateSemesterDetail(int id) {
            Console.WriteLine("Looking for the semester with ID" +  id);

            Semester semester = planner.getSemester(id);

            name.Text = semester.label;

            IList<Class> children = planner.getSemesterChildren(id).ToList<Class>();

            ClassListAdapter childAdapter = new ClassListAdapter(this, children);

            childrenList.Adapter = childAdapter;

        }

        private void populateSchoolYearDetail(int id) {
            SchoolYear sy = planner.getSchoolYear(id);

            name.Text = sy.label;


            IList<Semester> children = planner.getSchoolYearChildren(id).ToList<Semester>();

            Graduate.Droid.ListAdapters.SemesterListAdapter childAdapter = new ListAdapters.SemesterListAdapter(this, children);

            childrenList.Adapter = childAdapter;
        }

        private void populateClassDetail(int id)
        {

            Class c = planner.getClass(id);

            name.Text = c.label;


            

        }





    }
}