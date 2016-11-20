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

using Graduate.Core.Data.Models;
using Graduate.Core;

namespace Graduate.Droid
{
    [Activity(Label = "GraduateEntityEntryActivity")]
    public class GraduateEntityEntryActivity : Activity
    {
        EditText id;
        EditText fid;
        EditText label;
        Button save;
        Button cancel;
        int saveType = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GraduateEntityEntryActivity);

            findViews();
            handleEvents();

            saveType = Intent.Extras.GetInt("type");
        }

        private void findViews() {
            id = FindViewById<EditText>(Resource.Id.editTextID);
            fid = FindViewById<EditText>(Resource.Id.editTextFID);
            label = FindViewById<EditText>(Resource.Id.editTextLabel);
            save = FindViewById<Button>(Resource.Id.buttonSave);
            cancel = FindViewById<Button>(Resource.Id.buttonCancel);
        }

        private void handleEvents() {

            save.Click += Save_Click;
            cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {

            SetResult(Result.Ok, null);
            Finish();
        }

        private void saveSemester() {
            Semester sem = new Semester();
            sem.FId = Convert.ToInt32(fid.Text);
            sem.label = label.Text;
            GraduateApp.Current.planner.saveSemester(sem);
        }

        private void saveSchoolYear() {
            SchoolYear sy = new SchoolYear();
            sy.label = label.Text;
            GraduateApp.Current.planner.saveSchoolYear(sy);

        }

        private void saveClass() {

            Class c = new Class();
            c.FId = Convert.ToInt32(fid.Text);
            c.label = label.Text;
            GraduateApp.Current.planner.saveClass(c);

        }

        private void Save_Click(object sender, EventArgs e)
        {
            switch (saveType)
            {
                case 1:
                    Console.WriteLine("SchoolYear Recieved");
                    saveSchoolYear();
                    SetResult(Result.Ok, null);
                    break;
                case 2:
                    Console.WriteLine("Semester Recieved");
                    saveSemester();
                    SetResult(Result.Ok, null);
                    break;
                case 3:
                    Console.WriteLine("Class Recieved");
                    saveClass();
                    SetResult(Result.Ok, null);
                    break;
                default:
                    Console.WriteLine("Unknown Recieved");
                    break;
            }



            SetResult(Result.Ok, null);            
            Finish();
        }
    }
}