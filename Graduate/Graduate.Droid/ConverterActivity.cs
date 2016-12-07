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
            letter.ItemSelected += Letter_ItemSelected;            
            percent.Click += Percent_Click;
            letter.Click += Letter_Click;
        
        }

        private void Letter_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            String letterEntry = letter.Text;

            Grade grade = gradeConverter.convertLetter(letterEntry);

            Console.WriteLine("Printing Returned Grade");
            Console.WriteLine(grade.ToString());

            percent.Text = grade.Percent.ToString();
            gpa.Text = grade.GPA.ToString();
        }

        private void Letter_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            String letterEntry = letter.Text;

            Grade grade = gradeConverter.convertLetter(letterEntry);

            Console.WriteLine("Printing Returned Grade");
            Console.WriteLine(grade.ToString());

            percent.Text = grade.Percent.ToString();
            gpa.Text = grade.GPA.ToString();
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

            Console.WriteLine("Printing Returned Grade");
            Console.WriteLine(grade.ToString());

            letter.Text = grade.Letter;
            gpa.Text = grade.GPA.ToString();
        }
    }
}