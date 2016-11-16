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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GraduateEntityEntryActivity);

            findViews();
            handleEvents();
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
            Finish();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Semester sem = new Semester();
            sem.FId = Convert.ToInt32(fid.Text);
           
            sem.label = label.Text;

            GraduateApp.Current.planner.saveSemester(sem);

            Finish();
        }
    }
}