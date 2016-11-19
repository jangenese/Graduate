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
        private EditText parentEntry;
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
            cancelButton = fragmentView.FindViewById<Button>(Resource.Id.buttonCancel);
            saveButton = fragmentView.FindViewById<Button>(Resource.Id.buttonSave);

            parentRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutParentRow);
            classRow = fragmentView.FindViewById<LinearLayout>(Resource.Id.linearLayoutClassRow);
    
        }



        private void handleEvents()
        {
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dismiss();
            Toast.MakeText(Activity, "Cancelled", ToastLength.Short).Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
                     
            saveEntry(type);
            Dismiss();
            Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
        }

        private void saveSchoolYear() {

            SchoolYear sy = new SchoolYear() { };
            sy.label = entry.Text.ToString();
            planner.saveSchoolYear(sy);                    
        }

        private void saveSemester() {
            Semester sem = new Semester();
            sem.label = entry.Text.ToString();
            sem.FId = 1;
            planner.saveSemester(sem);
        }

        private void saveClass() {
            Class c = new Class();
            c.label = entry.Text.ToString();
            c.grade = 4;
            c.goalGrade = 4;
            c.FId = 1;

            planner.saveClass(c);
            
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
            parentRow.Visibility = ViewStates.Gone;
            classRow.Visibility = ViewStates.Gone;
        }

        private void modifySemesterForm() {
            classRow.Visibility = ViewStates.Gone;
        }

        private void modifyClassForm() {
            
        }


        }
    }
