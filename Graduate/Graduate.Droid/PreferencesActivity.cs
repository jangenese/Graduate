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
    [Activity(Label = "PreferencesActivity")]
    public class PreferencesActivity : Activity
    {
        Spinner spinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PreferencesActivity);

            Button change = FindViewById<Button>(Resource.Id.buttonChange);

            change.Click += Change_Click;
            
        }

        private void Change_Click(object sender, EventArgs e)
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Feature Coming Soon");
            dialog.SetMessage("Thank You");
            dialog.Show();
        }
    }
}
