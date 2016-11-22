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

using Graduate.Core.Data.Models;
using Graduate.Core;


namespace Graduate.Droid.Fragments
{
    public class NewEntryDialogFragment : DialogFragment
    {

        private TextView title;
        private TextView parentType;
        private AutoCompleteTextView parentEntry;
        private TextView entryType;
        private EditText entry;
        private TextView grade;
        private EditText gradeEntry;
        private TextView credits;
        private EditText creditsEntry;
        private Button cancelButton;
        private Button saveButton;
        private Planner planner;

        private LinearLayout parentRow;
        private LinearLayout classRow;

        private int parentPosition = 0;


        private View fragmentView;
        public int type { get; set; } = 0;
        public static NewEntryDialogFragment NewInstance(Bundle bundle)
        {
            NewEntryDialogFragment fragment = new NewEntryDialogFragment();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            // Use this to return your custom view for this Fragment
            fragmentView = inflater.Inflate(Resource.Layout.NewEntryDialogFragment, container, false);
            planner = GraduateApp.Current.planner;

            findViews();
            populateAutoCompleteLists();
            handleEvents();

            checkFormType(type);

            return fragmentView;
        }


        private void findViews()
        {
            

            title = fragmentView.FindViewById<TextView>(Resource.Id.textViewTitle);
            parentType = fragmentView.FindViewById<TextView>(Resource.Id.textViewParent);
            parentEntry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewParentEntry);
            entryType = fragmentView.FindViewById<TextView>(Resource.Id.textViewLabel);
            entry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewLabelEntry);
            grade = fragmentView.FindViewById<TextView>(Resource.Id.textViewGrade);
            gradeEntry = fragmentView.FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewGradeEntry);
            credits = fragmentView.FindViewById<TextView>(Resource.Id.textViewCreditsLabel);
            creditsEntry = fragmentView.FindViewById<EditText>(Resource.Id.editTextCreditsEntry);
            cancelButton = fragmentView.FindViewById<Button>(Resource.Id.buttonCancel);
            saveButton = fragmentView.FindViewById<Button>(Resource.Id.buttonSave);

            parentRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutParentRow);
            classRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutClassRow);
    
        }



        private void handleEvents()
        {
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;
            parentEntry.ItemSelected += ParentEntry_ItemSelected;
        }

        private void ParentEntry_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            parentPosition = e.Position;            
        }

        private void populateAutoCompleteLists() {
            //var parentEntryOptions = new String[] { "Hello", "Hey", "Heja", "Hi", "Hola", "Bonjour", "Gday", "Goodbye", "Sayonara", "Farewell", "Adios" };

            var parentEntryOptions = planner.getAllSchoolYearLabels();

           ArrayAdapter parentEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, parentEntryOptions);
            
           parentEntry.Adapter = parentEntryAdapter;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dismiss();
            Toast.MakeText(Activity, "Cancelled", ToastLength.Short).Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
                     
            saveEntry(type);          

            this.TargetFragment.OnActivityResult(TargetRequestCode, Result.Ok, this.Activity.Intent);

            Dismiss();
            Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
        }

        private void saveSchoolYear() {
            planner.saveSchoolYear(entry.Text.ToString());                   
        }

        private void saveSemester() {
            int i = parentPosition + 1;

            Console.WriteLine("\n\n\n\n\n\n Printing Position" + parentEntry.Text);
            Console.WriteLine(i.ToString());
            
            String test = i.ToString();
            planner.saveSemester(test, entry.Text.ToString());
        }

        private void saveClass() {   
            planner.saveClass("1", entry.Text, gradeEntry.Text, creditsEntry.Text, true);
        }

        private void saveEntry(int type)
        {
            switch (type)
            {
                case 1:

                    saveSchoolYear();

                    break;
                case 2:

                    saveSemester();

                    break;
                case 3:
                    ;
                    saveClass();
                    break;
                default:

                    break;
            }
        }

        private void checkFormType(int type) {
            switch (type)
            {
                case 1:

                    modifySchoolYearForm();

                    break;
                case 2:

                    modifySemesterForm();

                    break;
                case 3:
                    ;
                    modifyClassForm();
                    break;
                default:

                    break;
            }
        }

        private void modifySchoolYearForm() {
            title.Text = "New Entry: SchoolYear";
            entryType.Text = "School Year";
            entry.Hint = "YYYY - YYYY";
            parentRow.Visibility = ViewStates.Gone;
            classRow.Visibility = ViewStates.Gone;
        }

        private void modifySemesterForm() {
            title.Text = "New Entry: Semester";
            parentType.Text = "School Year";
            entryType.Text = "Semester";
            entry.Hint = "Season YYYY";
            classRow.Visibility = ViewStates.Gone;
        }

        private void modifyClassForm() {
            title.Text = "New Entry: Class";
            parentType.Text = "Semester";
            entryType.Text = "Class";
            entry.Hint = "ABCD - 1234";
        }


        }
    }
