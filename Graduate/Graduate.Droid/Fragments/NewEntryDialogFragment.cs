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
using Graduate.Droid.ListAdapters;


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
        private CheckBox checkbox;
        private Button cancelButton;
        private Button saveButton;
        private Planner planner;
        

        private LinearLayout parentRow;
        private LinearLayout classRow;
        private LinearLayout checkBoxRow;

        private int parentPosition = 0;
        private Boolean status = false;


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
            checkbox = fragmentView.FindViewById<CheckBox>(Resource.Id.checkBoxCompleted);
            cancelButton = fragmentView.FindViewById<Button>(Resource.Id.buttonCancel);
            saveButton = fragmentView.FindViewById<Button>(Resource.Id.buttonSave);

            parentRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutParentRow);
            classRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutMainClass);
            checkBoxRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutCheckBoxRow);
            
    
        }



        private void handleEvents()
        {
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;
            checkbox.Click += Checkbox_Click;            
            
        }

        private void Checkbox_Click(object sender, EventArgs e)
        {
            if (checkbox.Checked)
            {
                status = true;
                grade.Text = "Grade";
            }
            else {
                status = false;
                grade.Text = "Goal";
            }
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
            String fid = getSchoolYearParentID(parentEntry.Text, planner.getAllSchoolYears());
            planner.saveSemester(fid, entry.Text.ToString());
        }

        private void saveClass() {
            String fid = getSemesterParentID(parentEntry.Text, planner.getAllSemesters());
            planner.saveClass(fid, entry.Text, gradeEntry.Text, creditsEntry.Text, status);
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
            checkBoxRow.Visibility = ViewStates.Gone;
        }

        private void modifySemesterForm() {
            var parentEntryOptions = planner.getAllSchoolYearLabels();
            ArrayAdapter parentEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, parentEntryOptions);

            
            

            parentEntry.Adapter = parentEntryAdapter;            
            title.Text = "New Entry: Semester";
            parentType.Text = "School Year";
            entryType.Text = "Semester";
            entry.Hint = "Season YYYY";
            classRow.Visibility = ViewStates.Gone;
            checkBoxRow.Visibility = ViewStates.Gone;
        }

        private void modifyClassForm() {
            var parentEntryOptions = planner.getAllSemesterLabels();
            ArrayAdapter parentEntryAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, parentEntryOptions);
            parentEntry.Adapter = parentEntryAdapter;
            title.Text = "New Entry: Class";
            parentType.Text = "Semester";
            entryType.Text = "Class";
            entry.Hint = "ABCD - 1234";
        }

        private String getParentPosition() {
            

            int i = parentPosition + 1;

            return i.ToString();
        }

        private String getSchoolYearParentID(String label, IList<SchoolYear> list) {
            int i = 0;            
            foreach (SchoolYear sy in list) {
                if (label == sy.label) {
                    i = sy.Id;
                }
            }

            return i.ToString();
        }

        private String getSemesterParentID(String label, IList<Semester> list)
        {
            int i = 0;
            foreach (Semester sem in list)
            {
                if (label == sem.label)
                {
                    i = sem.Id;
                }
            }

            return i.ToString();
        }


    }
    }
