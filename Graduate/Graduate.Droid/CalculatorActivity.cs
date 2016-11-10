using System;


using Android.App;

using Android.OS;

using Android.Views;
using Android.Widget;
using Graduate.Core;



namespace Graduate.Droid
{
    [Activity(Label = "Calculator")]
    public class CalculatorActivity : Activity
    {
        private Button addButton;
        private EditText percent;
        private EditText letter;
        private EditText gpa;
        private int rows = 1;

      Calculator calculator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CalculatorActivity);

            findViews();
            handleEvents();

        calculator = GraduateApp.Current.calculator;

        
          Console.WriteLine(calculator.toStringContent());
            Console.WriteLine("asdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsaasdfffffffasdfasdfasdfasdfasdfsa");

            Graduate.Core.Models.Grade grade = calculator.getPercent("100");

            Console.WriteLine("Searrch returned back to calc activity");
            Console.WriteLine(grade.ToString());

        }

        private void findViews()
        {
            addButton = FindViewById<Button>(Resource.Id.buttonAdd);
            letter = FindViewById<EditText>(Resource.Id.editTextLetterRow1);
            percent = FindViewById<EditText>(Resource.Id.editTextPercentRow1);
            gpa = FindViewById<EditText>(Resource.Id.editTextGPARow1);
        }

        private void handleEvents()
        {
            addButton.Click += AddButton_Click;
           percent.AfterTextChanged += Percent_AfterTextChanged;
         
        }

       

        

        private void Percent_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
          String percentEntry = percent.Text.ToString();

           Graduate.Core.Models.Grade grade =  calculator.getPercent("80");

            Console.WriteLine("Searrch returned back to calc activity sakfdsjlhglksdjhglksjdhfglaksjdhfglkjs dhfglksjdfhglskdfjhgdfsk\n");
            Console.WriteLine(grade.ToString());

           gpa.Text = grade.GPA.ToString();
            letter.Text = grade.Letter;
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