using System;

using Android.App;
using Android.Content;

using Android.Widget;
using Android.OS;





using System.IO;
using Android.Views;

namespace Graduate.Droid
{

   [ Activity(Label = "MainActivity")]
    
    public class MainActivity : Activity
	{
        private Button calculatorButton;
        private Button plannerButton;       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Title = "Graduate";

            SetContentView(Resource.Layout.MainActivity);



            findViews();
            handleEvents();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        private void findViews()
        {
            calculatorButton = FindViewById<Button>(Resource.Id.buttonCalculator);
            plannerButton = FindViewById<Button>(Resource.Id.buttonPlanner);
        }

        private void handleEvents()
        {
            calculatorButton.Click += calculatorButton_Click;
            plannerButton.Click += plannerButton_Click;

        }

        private void calculatorButton_Click(object sender, EventArgs e)
        {
            //  var intent = new Intent(this, typeof(CalculatorActivity));

            var intent = new Intent(this, typeof(ConverterActivity));
            StartActivity(intent);
        }

        private void plannerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PlannerActivity_Main));
            StartActivity(intent);
        }




    }
}


