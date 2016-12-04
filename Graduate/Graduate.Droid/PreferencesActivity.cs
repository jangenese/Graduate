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

            var dialog = new AlertDialog.Builder(this);
            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected +=  (s,e) => {
                string firstItem = spinner.SelectedItem.ToString();
                if (firstItem.Equals(spinner.SelectedItem.ToString()))
                {
                    dialog.SetTitle("Feature Coming Soon");
                    dialog.SetMessage("Schema Selection Coming Soon\n Thank You!");
                    dialog.Show();
                }
                else
                { /*do something*/ }

            };
            
        }
    }
}
