using System;
using System.IO;
using SQLite;
using Android.App;
using Graduate.Core;
using Android.Content;

namespace Graduate.Droid
{
    [Application]
    public class GraduateApp : Application
    {
        private Boolean firstRun;
        ISharedPreferences prefs = null;
        public static GraduateApp Current { get; private set; }

        public Planner planner { get; set; }
        public GradeConverter converter { get; set; }
        SQLiteConnection conn;
        public GraduateApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            prefs = Application.Context.GetSharedPreferences("Graduate.Droid", FileCreationMode.Private);

            var sqliteFilename = "Graduate.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new SQLiteConnection(path);


            //  calculator = new Calculator(conn);
            planner = new Planner(conn);

            converter = new GradeConverter(conn);
            firstRun = prefs.GetBoolean("firstRun", true);

            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n"); Console.WriteLine("First run check\n"); Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");
            Console.WriteLine("First run check\n");

            if (firstRun)
            {

                Console.WriteLine("Hi, this is my first run");


                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean("firstRun", false);
                editor.Apply();
            }

            Console.WriteLine("Not a a first run");


        }

    }
}

