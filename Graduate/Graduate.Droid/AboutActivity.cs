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
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AboutActivity);

            TextView t = (TextView)FindViewById(Resource.Id.textView2);
            t.Text = "App Version 1.00 \n\nDeveloped By Tough Cookie Team\nTerm Project For COMP3504 - MRU"
                + "\n\nJan Genese - Lead Programmer/Developer, \nDaniel Truong - Design/Marketing\n\n"+
                "For any questions or concerns please contact Tough Cookie at ToughCookie@gmail.com";
                

        }
    }
}
