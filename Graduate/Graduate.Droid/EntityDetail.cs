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
using com.refractored.fab;
using Graduate.Droid.Fragments;



namespace Graduate.Droid
{
    [Activity(Label = "EntityDetail")]
    public class EntityDetail : Activity, IDialogInterfaceOnDismissListener
    {
        private TextView label;
        private TextView parentLabel;
        private TextView credits;
        private TextView status;
        private TextView grade;
        private ListView childrenList;
        private int selectedID;
        private Planner planner;


        private FloatingActionButton fab;

        private GraduateEntityBase entity = null;
        private int childrenPosition;
        private int childrenType = 0;

        



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EntityDetail);

            planner = GraduateApp.Current.planner;

            selectedID = Intent.Extras.GetInt("selectedEntityID");

            findViews();
            fab.AttachToListView(childrenList);
            handleEvents();

            label.Text = selectedID.ToString();            
            populatePage();

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok) {
                Console.WriteLine("Hello");
            }
            Console.WriteLine("On Result is being exceuted"); 
        }

        

        void IDialogInterfaceOnDismissListener.OnDismiss(IDialogInterface dialog)
        {
            populatePage(); 
}


        private void populatePage() {
            switch (Intent.Extras.GetInt("type"))
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
            label = FindViewById<TextView>(Resource.Id.textViewLabel);
            parentLabel = FindViewById<TextView>(Resource.Id.textViewParentLabel);
            credits = FindViewById<TextView>(Resource.Id.textViewCredits);
            status = FindViewById<TextView>(Resource.Id.textViewStatusLabel);
            grade = FindViewById<TextView>(Resource.Id.textViewGrade);
            childrenList = FindViewById<ListView>(Resource.Id.listViewChildItems);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
        }

        private void handleEvents() {
            childrenList.ItemClick += ChildrenList_ItemClick;
            fab.Click += Fab_Click;            
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            showEntryForm();
           

        }

        

        private void ChildrenList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {


            if (childrenType != 4) {
                childrenPosition = e.Position;

                var intent = new Intent();
                intent.SetClass(this, typeof(EntityDetail));
                intent.PutExtra("type", childrenType);
                intent.PutExtra("selectedEntityID", entity.Id);

                StartActivityForResult(intent, 100);
            }            
        }

        private void populateSemesterDetail(int id) {
            SemesterView semester = planner.getSemester(id.ToString());
            label.Text = semester.label;
            parentLabel.Text = semester.parentLabel;
            credits.Text = semester.credits;
            status.Text = semester.status;
            grade.Text = semester.grade;

            IList<Class> children = semester.children;            

            ClassListAdapter childAdapter = new ClassListAdapter(this, children);
          

            childrenList.Adapter = childAdapter;
            childrenType = 3;
            try{
                entity = children[childrenPosition];
            }catch {

            }

        }

        private void populateSchoolYearDetail(int id) {
            SchoolYearView sy = planner.getSchoolYear(id.ToString());

            label.Text = sy.label;
            parentLabel.Text = sy.parentLabel;
            credits.Text = sy.credits;
            status.Text = sy.status;
            grade.Text = sy.grade;


            IList<Semester> children = sy.children;

            Graduate.Droid.ListAdapters.SemesterListAdapter childAdapter = new ListAdapters.SemesterListAdapter(this, children);

            childrenList.Adapter = childAdapter;
            childrenType = 2;


            try
            {
                entity = children[childrenPosition];
            }
            catch {

            }
        }

        private void populateClassDetail(int id)
        {
            ClassView c = planner.getClass(id.ToString());

            label.Text = c.label;
            parentLabel.Text = c.parentLabel;
            credits.Text = c.credits;
            parentLabel.Text = c.parentLabel;
            status.Text = c.status;
            grade.Text = c.grade;

            IList<ClassActivity> children = c.children;

            ClassActivityListAdapter childAdapter = new ClassActivityListAdapter(this, children);

            childrenList.Adapter = childAdapter;

            childrenType = 4;
            /*

            try
            {
                entity = children[childrenPosition];
            }
            catch
            {

            }
            */
        }

        private void showEntryForm()
        {
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
            dialogFrag.parentId = selectedID;
            //dialogFrag.SetTargetFragment(this, 1);
           

            
           dialogFrag.type = childrenType;
            dialogFrag.fromParent = true;
            dialogFrag.Show(ft, "dialog");
          
        }


    }

       
    }
