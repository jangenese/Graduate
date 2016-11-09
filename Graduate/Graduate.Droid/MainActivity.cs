using System;

using Android.App;
using Android.Content;

using Android.Widget;
using Android.OS;





using System.IO;


namespace Graduate.Droid
{

   [ Activity(Label = "MainActivity")]
    
    public class MainActivity : Activity
	{
        private Button calculatorButton;
        private Button plannerButton;
        private Button dummyButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainActivity);



            findViews();
            handleEvents();
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
            var intent = new Intent(this, typeof(CalculatorActivity));
            StartActivity(intent);
        }

        private void plannerButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PlannerActivity_Main));
            StartActivity(intent);
        }


        private void initializeDB()
        {

            String dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGraduate.db3");

            var stream = Android.App.Application.Context.Assets.Open("gpa.txt");


            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");

            Console.WriteLine("Initializing");
            Console.WriteLine("Initializing");
        }




    }
}


