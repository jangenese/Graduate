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


            String rightsInfo = "People graphic by <a href=http://www.flaticon.com/authors/freepik>Freepik</a> from <a href=http://www.flaticon.com/>Flaticon</a> is licensed under <a href=http://creativecommons.org/licenses/by/3.0/ title=Creative Commons BY 3.0>CC BY 3.0</a>. Made with <a href=http://logomakr.com title=Logo Maker>Logo Maker</a>";
            TextView rights = FindViewById<TextView>(Resource.Id.textViewRights);
            rights.Text = Android.Text.Html.FromHtml(rightsInfo).ToString();

            




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
