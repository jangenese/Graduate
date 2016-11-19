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

            findViews();
            handleEvents();

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
            saveSemester();
            Dismiss();
            Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
        }

        private void saveSchoolYear() {
            Console.WriteLine("Saving SchoolYear");
        }

        private void saveSemester() {
            Console.WriteLine("Saving Semester");
        }

        private void saveClass() {
            Console.WriteLine("Saving Semester");
        }
    }
}