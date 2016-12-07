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
    [Activity(Label = "AboutActivity")]
    public class AboutActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "About";
            base.OnCreate(savedInstanceState);




            SetContentView(Resource.Layout.AboutActivity);



            TextView t = (TextView)FindViewById(Resource.Id.textView2);
            t.Text = "App Version 1.00 \n\nDeveloped By Tough Cookie Team\nTerm Project For COMP3504 - MRU"
                + "\n\nJan Genese - Lead Programmer/Developer, \nDaniel Truong - Design/Marketing\n\n" +
                "For any questions or concerns please contact Tough Cookie at ToughCookie@gmail.com";


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
