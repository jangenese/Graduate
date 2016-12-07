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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "Preferences";


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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.menu_preferences:
                    var preferenceIntent = new Intent(this, typeof(PreferencesActivity));
                    StartActivity(preferenceIntent);
                    break;
                case Resource.Id.menu_about:
                    var aboutIntent = new Intent(this, typeof(AboutActivity));
                    StartActivity(aboutIntent);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}