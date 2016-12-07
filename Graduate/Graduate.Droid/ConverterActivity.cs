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

        EditText currentGPA;
        EditText creditsRemaining;
        EditText creditsRequired;
        EditText desiredGPA;
        TextView result;
        TextView statement;

        Button calculate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.Title = "Calculator";
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

            currentGPA = FindViewById<EditText>(Resource.Id.editTextCurrentGPA);
            creditsRemaining = FindViewById<EditText>(Resource.Id.editTextCreditsRemaining);
            creditsRequired = FindViewById<EditText>(Resource.Id.editTextCreditsRequired);
            desiredGPA = FindViewById<EditText>(Resource.Id.editTextDesiredGPA);

            result = FindViewById<TextView>(Resource.Id.textViewResult);
            statement = FindViewById<TextView>(Resource.Id.textViewStatement);

            calculate = FindViewById<Button>(Resource.Id.buttonCalculate);

        }

        private void handleEvents() {    

            percent.AfterTextChanged += Percent_AfterTextChanged;
            letter.ItemClick += Letter_ItemClick;                     
            percent.Click += Percent_Click;
            letter.Click += Letter_Click;



            currentGPA.Click += CurrentGPA_Click;

            creditsRemaining.Click += CreditsRemaining_Click;

            creditsRequired.Click += CreditsRequired_Click;

            desiredGPA.Click += DesiredGPA_Click;

            calculate.Click += Calculate_Click;
        }

        private void Calculate_Click(object sender, EventArgs e)
        {

            String remaining = creditsRemaining.Text;      

            double estimatedResult = gradeConverter.estimateGPA(currentGPA.Text, creditsRemaining.Text, creditsRequired.Text, desiredGPA.Text);
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine("Printing Result");
            Console.WriteLine(estimatedResult);
            String estimatedresultText = formatGPA(estimatedResult);

            String stringStatement = "You need to average a " + estimatedresultText + " over your final " + remaining + " Credits to graduate with your desired GPA.";

            result.Text = estimatedresultText;
            statement.Text = stringStatement;
        }

        private void DesiredGPA_Click(object sender, EventArgs e)
        {
            currentGPA.Focusable = true;
            currentGPA.FocusableInTouchMode = true;
            creditsRemaining.Focusable = true;
            creditsRemaining.FocusableInTouchMode = true;
            creditsRequired.Focusable = true;
            creditsRequired.FocusableInTouchMode = true;
            desiredGPA.Focusable = true;
            desiredGPA.FocusableInTouchMode = true;

            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = false;
            letter.FocusableInTouchMode = false;
        }

        private void CreditsRequired_Click(object sender, EventArgs e)
        {
            currentGPA.Focusable = true;
            currentGPA.FocusableInTouchMode = true;
            creditsRemaining.Focusable = true;
            creditsRemaining.FocusableInTouchMode = true;
            creditsRequired.Focusable = true;
            creditsRequired.FocusableInTouchMode = true;
            desiredGPA.Focusable = true;
            desiredGPA.FocusableInTouchMode = true;

            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = false;
            letter.FocusableInTouchMode = false;
        }

        private void CreditsRemaining_Click(object sender, EventArgs e)
        {
            currentGPA.Focusable = true;
            currentGPA.FocusableInTouchMode = true;
            creditsRemaining.Focusable = true;
            creditsRemaining.FocusableInTouchMode = true;
            creditsRequired.Focusable = true;
            creditsRequired.FocusableInTouchMode = true;
            desiredGPA.Focusable = true;
            desiredGPA.FocusableInTouchMode = true;

            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = false;
            letter.FocusableInTouchMode = false;
        }

        private void CurrentGPA_Click(object sender, EventArgs e)
        {
            currentGPA.Focusable = true;
            currentGPA.FocusableInTouchMode = true;
            creditsRemaining.Focusable = true;
            creditsRemaining.FocusableInTouchMode = true;
            creditsRequired.Focusable = true;
            creditsRequired.FocusableInTouchMode = true;
            desiredGPA.Focusable = true;
            desiredGPA.FocusableInTouchMode = true;

            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = false;
            letter.FocusableInTouchMode = false;
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
            currentGPA.Focusable = false;
            currentGPA.FocusableInTouchMode = false;
            creditsRemaining.Focusable = false;
            creditsRemaining.FocusableInTouchMode = false;
            creditsRequired.Focusable = false;
            creditsRequired.FocusableInTouchMode = false;
            desiredGPA.Focusable = false;
            desiredGPA.FocusableInTouchMode = false;

            percent.Focusable = false;
            percent.FocusableInTouchMode = false;

            letter.Focusable = true;
            letter.FocusableInTouchMode = true;
        }

        private void Percent_Click(object sender, EventArgs e)
        {
            currentGPA.Focusable = false;
            currentGPA.FocusableInTouchMode = false;
            creditsRemaining.Focusable = false;
            creditsRemaining.FocusableInTouchMode = false;
            creditsRequired.Focusable = false;
            creditsRequired.FocusableInTouchMode = false;
            desiredGPA.Focusable = false;
            desiredGPA.FocusableInTouchMode = false;

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