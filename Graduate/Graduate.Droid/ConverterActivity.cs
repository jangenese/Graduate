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

using Android.Views.InputMethods;

namespace Graduate.Droid
{
    [Activity(Label = "ConverterActivity")]
    public class ConverterActivity : Activity
    {
        GradeConverter gradeConverter;
        EditText percent;
        AutoCompleteTextView letter;
        TextView gpa;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Converter);

            gradeConverter = GraduateApp.Current.converter;

            findViews();
            handleEvents();

            var gradeEntryOptions = GraduateApp.Current.planner.getAllLetterGrades();            
            ArrayAdapter gradeEntryAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, gradeEntryOptions);
            letter.Adapter = gradeEntryAdapter;


        }

        private void findViews() {
            percent = FindViewById<EditText>(Resource.Id.editTextConverterPercenr);
            letter = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextViewConverterLetter);
            gpa = FindViewById<TextView>(Resource.Id.textViewConverterGPA);
        }

        private void handleEvents() {    

            percent.AfterTextChanged += Percent_AfterTextChanged;
            letter.ItemClick += Letter_ItemClick;                     
            percent.Click += Percent_Click;
            letter.Click += Letter_Click;
        
        }

        private void Letter_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            String letterEntry = letter.Text;

            Grade grade = gradeConverter.convertLetter(letterEntry);
            percent.Text = grade.Percent.ToString();
            gpa.Text = formatGPA(grade.GPA);
        }
       

        private void Letter_Click(object sender, EventArgs e)
        {
            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = false;
            letter.FocusableInTouchMode = true;
        }

        private void Percent_Click(object sender, EventArgs e)
        {
            letter.Focusable = false;
            letter.FocusableInTouchMode = false;

            percent.Focusable = true;
            percent.FocusableInTouchMode = true;
        }

        private void Percent_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            String percentEntry = percent.Text;

            Grade grade = gradeConverter.convertPercent(percentEntry);           

            letter.Text = grade.Letter;
            gpa.Text = formatGPA(grade.GPA);
        }

        private String formatGPA(double gpa)
        {
            return String.Format("{0:0.00}", gpa);

        }
    }
}