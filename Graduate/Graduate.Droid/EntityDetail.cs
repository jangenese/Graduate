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

namespace Graduate.Droid
{
    [Activity(Label = "EntityDetail")]
    public class EntityDetail : Activity
    {
        private TextView label;
        private int selectedID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EntityDetail);

            selectedID = Intent.Extras.GetInt("selectedEntityID");

            findViews();

            label.Text = selectedID.ToString();
        }

        private void findViews() {
            label = FindViewById<TextView>(Resource.Id.textViewLabel);
        }



       
    }
}