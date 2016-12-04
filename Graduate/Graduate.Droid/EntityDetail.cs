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

using Graduate.Core.View.Model;
using com.refractored.fab;
using Graduate.Droid.Fragments;

namespace Graduate.Droid
{
    [Activity(Label = "EntityDetail")]
    public class EntityDetail : Activity, IDialogInterfaceOnDismissListener
    {
        //Android Views ******
        private TextView label;
        private TextView parentLabel;
        private TextView credits;
        private TextView status;
        private TextView grade;
        private ListView childrenList;
        private FloatingActionButton fab;

        private TextView headerlabel;
        private TextView headerstatus;
        private TextView headergrade;
        //******Android Views

        private Planner planner;

        private int selectedID;                                 //selectedID From Fragment List
        private IList<GraduateEntityBase> children = null;      //list of children in base form


        private int childrenType = 0;                           //children type set by populator

        private String activityTitle = "";                      //Action Bar Title

        private IList<ClassActivity> classChildrenList = null;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EntityDetail);

            planner = GraduateApp.Current.planner;

            selectedID = Intent.Extras.GetInt("selectedEntityID");

            findViews();
            fab.AttachToListView(childrenList);
            handleEvents();

            populatePage();
        }

        protected override void OnResume()
        {
            base.OnResume();
            populatePage();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                Console.WriteLine("Hello");
            }
            Console.WriteLine("On Result is being exceuted");
        }
        /*
        Creates the overflow button on actionbar
        */
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.detail_menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        /*
        Handler for the selected item from the overflow menu
        */
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.edit:
                    editThisItem(Intent.Extras.GetInt("type"), Intent.Extras.GetInt("selectedEntityID"));
                    break;
                case Resource.Id.delete:
                    displayDeleteAlert(Intent.Extras.GetInt("type"), selectedID);
                    break;
                case Resource.Id.menu_preferences:
                    var preferenceIntent = new Intent(this, typeof(PreferencesActivity));
                    StartActivity(preferenceIntent);
                    break;
                case Resource.Id.menu_about:
                    var aboutIntent = new Intent(this, typeof(AboutActivity));
                    StartActivity(aboutIntent);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        /*
        Checks if children new entry dialog has been dismissed and repopulates the page
        */
        void IDialogInterfaceOnDismissListener.OnDismiss(IDialogInterface dialog)
        {
            populatePage();
        }


        /*
        Populates the page by calling the appropriate method based on the object type passed in
       */
        private void populatePage()
        {
            switch (Intent.Extras.GetInt("type"))
            {
                case 1:
                    activityTitle = "SchoolYear Details";
                    populateSchoolYearDetail(selectedID);
                    break;
                case 2:
                    activityTitle = "Semester Details";
                    populateSemesterDetail(selectedID);
                    break;
                case 3:
                    activityTitle = "Class Details";
                    populateClassDetail(selectedID);
                    break;
                default:
                    Console.WriteLine("Unknown Recieved");
                    break;
            }
        }

        /*
        Finds the AndroidViews
        */
        private void findViews()
        {
            label = FindViewById<TextView>(Resource.Id.textViewLabel);
            parentLabel = FindViewById<TextView>(Resource.Id.textViewParentLabel);
            credits = FindViewById<TextView>(Resource.Id.textViewCreditsEntry);
            status = FindViewById<TextView>(Resource.Id.textViewStatusEntry);
            grade = FindViewById<TextView>(Resource.Id.textViewGradeEntry);
            childrenList = FindViewById<ListView>(Resource.Id.listViewChildItems);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            headerlabel = FindViewById<TextView>(Resource.Id.textViewHeaderLabel);
            headerstatus = FindViewById<TextView>(Resource.Id.textViewHeaderStatus);
            headergrade = FindViewById<TextView>(Resource.Id.textViewHeaderGrade);
        }

        /*
        Event Listeners defined
       */
        private void handleEvents()
        {
            childrenList.ItemClick += ChildrenList_ItemClick;
            childrenList.ItemLongClick += ChildrenList_ItemLongClick;
            fab.Click += Fab_Click;
        }

        private void ChildrenList_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            var entity = classChildrenList[e.Position];
            Console.WriteLine(entity.ToString());

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(entity.label);            
            alert.SetPositiveButton("Delete", (senderAlert, args) => { 
                displayDeleteAlert(4, entity.Id);
            });

            alert.SetNegativeButton("Edit", (senderAlert, args) => {
                editThisItem(4, entity.Id);
            });
            
            alert.SetNeutralButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Cancelled", ToastLength.Short).Show();
            });

            alert.Show();


        }

        /*
        Calls method showEntryForm when FAB Button is clicked
       */
        private void Fab_Click(object sender, EventArgs e)
        {
            showEntryForm();
        }


        /*
        Event handler for when a children item is clicked in the chilredn list
       */
        private void ChildrenList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (childrenType != 4)
            {                //checks if the children is of type activitiy which is not clickable                

                var entity = children[e.Position]; //takes base child enitity with the clicked position from the children list

                var intent = new Intent();
                intent.SetClass(this, typeof(EntityDetail));
                intent.PutExtra("type", childrenType);
                intent.PutExtra("selectedEntityID", entity.Id);

                StartActivityForResult(intent, 100);
            }
        }

        /*
        Populates the Detail Page for a Semester
        Parameter id is the entity ID
       */
        private void populateSemesterDetail(int id)
        {
            this.Title = activityTitle;                         //sets page title

            //Main Header Info *******
            SemesterView semester = planner.getSemester(id.ToString());
            label.Text = semester.label;
            parentLabel.Text = semester.parentLabel;
            credits.Text = semester.credits;
            status.Text = semester.status;
            grade.Text = semester.percentGrade;
            //******** Main Header Info

            //Body Info (Children) ****** 
            headerlabel.Text = "Class";

            children = semester.children.ToList<GraduateEntityBase>();

            ClassListAdapter childAdapter = new ClassListAdapter(this, semester.children);
            childrenList.Adapter = childAdapter;

            childrenType = 3;                                   //Sets children type to 3 for Class for when clicked

            insertFooter(Resource.Layout.SemesterAndSchoolYearFooterFragment);

            Console.WriteLine("Footer Added");

        }


        /*
        Populate page for entity type SchoolYear 
        Params id is the entity ID
        */
        private void populateSchoolYearDetail(int id)
        {
            this.Title = activityTitle;
            SchoolYearView sy = planner.getSchoolYear(id.ToString());

            //Populate Main Info (Header) ******
            label.Text = sy.label;
            parentLabel.Text = sy.parentLabel;
            credits.Text = sy.credits;
            status.Text = sy.status;
            grade.Text = sy.percentGrade;
            //******Populate Main Info

            //Populate Body Info (Children List)******
            headerlabel.Text = "Semester";
            children = sy.children.ToList<GraduateEntityBase>();
            SemesterListAdapter childAdapter = new ListAdapters.SemesterListAdapter(this, sy.children);
            childrenList.Adapter = childAdapter;
            childrenType = 2;                               //Sets childrentype to 2 for Semesters
                                                            //******Populate Body Info (Children List)

            insertFooter(Resource.Layout.SemesterAndSchoolYearFooterFragment);

            Console.WriteLine("Footer Added");
        }


        /*
            Populate page for entity type Class
            Param id is entityID
        */
        private void populateClassDetail(int id)
        {
            this.Title = activityTitle;
            ClassView c = planner.getClass(id.ToString());
            //Populate Main Info (Header) ******
            label.Text = c.label;
            parentLabel.Text = c.parentLabel;
            credits.Text = c.credits;
            parentLabel.Text = c.parentLabel;
            status.Text = c.status;
            grade.Text = c.percentGrade;
            //******Populate Main Info



            //Populate Body Info (Children List)******
            headerlabel.Text = "Actvities";
            headerstatus.Text = "Weight";
            classChildrenList = c.children;

            ClassActivityListAdapter childAdapter = new ClassActivityListAdapter(this, classChildrenList);
            childrenList.Adapter = childAdapter;
            childrenType = 4;                           //Sets childrentype to 4 for ClassActivities
                                                        //******Populate Body Info (Children List)





            //LinearLayout footerLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutFooter);
            //LinearLayout mainLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutDetailMain);
            //View fragmentView = LayoutInflater.Inflate(Resource.Layout.ClassFooterFragment, footerLayout, false);
            //footerLayout.AddView(fragmentView);

            insertFooter(Resource.Layout.ClassFooterFragment);

            TextView goalGrade = FindViewById<TextView>(Resource.Id.textViewFooterGoalGrade);
            TextView inpWeight = FindViewById<TextView>(Resource.Id.textViewFooterRemainingWeight);
            TextView neededGrade = FindViewById<TextView>(Resource.Id.textViewFooterNeedeGrade);
            goalGrade.Text = "A+";
            inpWeight.Text = "30%";
            neededGrade.Text = "76%";



            Console.WriteLine("Footer Added");
        }



        /*
        Shows the new Entry form by calling a dialog fragment 
        */
        private void showEntryForm()
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }

            ft.AddToBackStack(null);

            // Create and show the dialog.
            NewEntryDialogFragment dialogFrag = NewEntryDialogFragment.NewInstance(null);
            dialogFrag.parentId = selectedID;
            dialogFrag.type = childrenType;
            dialogFrag.fromParent = true;
            dialogFrag.Show(ft, "dialog");

        }
        /*
            Displays Delete Alert 
        */
        private void displayDeleteAlert(int type, int id) {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm delete");
            alert.SetMessage("Are you sure you want to delete this item?");
            alert.SetPositiveButton("Delete", (senderAlert, args) => {
                deleteThisItem(type, id);                                           //Calls Delete Handler
                Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();

                if (type != 4)
                {
                    Finish();
                }
                else {
                    populatePage();
                }
                
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }


        /*
                Checks what kind of item is being deleted and calls apropriate function 
        */
        private void deleteThisItem(int type, int itemId) {
            String id = itemId.ToString();          
            switch (type)
            {
                case 1:
                    deleteThisSchoolYear(id);
                    break;
                case 2:
                    deleteThisSemester(id);
                    break;
                case 3:
                    deleteThisClass(id);
                    break;
                case 4:
                    Console.WriteLine("Have to delete an Actitivty");
                    deleteThisActivity(id);
                    break;
                default:
                    Console.WriteLine("Unknown Recieved");
                    break;
            }
        }

        private void deleteThisClass(String id) {
            planner.deleteClass(id);
        }

        private void deleteThisSemester(String id) {
            planner.deleteSemester(id);
        }

        private void deleteThisSchoolYear(String id) {
            planner.deleteSchoolYear(id);
        }

        private void deleteThisActivity(String id) {
            planner.deleteClassActivity(id);
        }

        private void editThisItem(int type, int ID) {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }

            ft.AddToBackStack(null);

            // Create and show the dialog.

            EditDialogFragment editDialogFrag = EditDialogFragment.NewInstance(null);


            editDialogFrag.parentId = selectedID;
            editDialogFrag.type = type;
            editDialogFrag.entityID = ID;           
            editDialogFrag.Show(ft, "dialog");
        }


        private void insertFooter(int layout) {
            View footerView = getNewViewFromLayout(this.LayoutInflater, this.FindViewById<LinearLayout>(Resource.Id.linearLayoutFooter), null, layout);
            LinearLayout footerContainerLayout = this.FindViewById<LinearLayout>(Resource.Id.linearLayoutFooter);
            footerContainerLayout.AddView(footerView);
        }

        private View getNewViewFromLayout(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState, int layout) {
            View footerView = inflater.Inflate(layout, container, false);
            return footerView;
        }

    }


}
