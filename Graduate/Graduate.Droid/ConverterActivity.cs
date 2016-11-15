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
        EditText letter;
        TextView gpa;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Converter);

            gradeConverter = GraduateApp.Current.converter;

            findViews();
            handleEvents();
        }

        private void findViews() {
            percent = FindViewById<EditText>(Resource.Id.editTextPercent);
            letter = FindViewById<EditText>(Resource.Id.editTextLetter);
            gpa = FindViewById<TextView>(Resource.Id.textViewGPA);
        }

        private void handleEvents() {
            
            

            percent.AfterTextChanged += Percent_AfterTextChanged;
        
        }

             
       

       
        private void Percent_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            String percentEntry = percent.Text;

            Grade grade = gradeConverter.convertPercent(percentEntry);

            letter.Text = grade.Letter;
            gpa.Text = grade.GPA.ToString();
        }
    }
}