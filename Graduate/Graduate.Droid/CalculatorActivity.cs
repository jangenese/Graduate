using System;


using Android.App;

using Android.OS;

using Android.Views;
using Android.Widget;



namespace Graduate.Droid
{
    [Activity(Label = "Calculator")]
    public class CalculatorActivity : Activity
    {
        private Button addButton;
        private int rows = 1;

      //  Graduate.core.Calculator calculator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CalculatorActivity);

            findViews();
            handleEvents();

        //    calculator = GraduateApp.Current.Calculator;

         //   Console.WriteLine(calculator.ToString());
        //    Console.WriteLine(calculator.toStringContent());
        //    Console.WriteLine("asdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsa");

        }

        private void findViews()
        {
            addButton = FindViewById<Button>(Resource.Id.buttonAdd);
        }

        private void handleEvents()
        {
            addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            View newView;

            newView = getNewRowView(this.LayoutInflater, this.FindViewById<LinearLayout>(Resource.Id.linearLayoutMain), null);

            addNewRow(newView);
        }



        private View getNewRowView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rowView = inflater.Inflate(Resource.Layout.CalculatorRowFragment, container, false);
            return rowView;
        }

        private void addNewRow(View newView)
        {

            rows++;

            LinearLayout mainLayout = this.FindViewById<LinearLayout>(Resource.Id.LinearLayoutScrollMain);

            mainLayout.FindViewById<TextView>(Resource.Id.textViewEntryRow1).Text = rows.ToString();

            mainLayout.AddView(newView);
        }
    }
}