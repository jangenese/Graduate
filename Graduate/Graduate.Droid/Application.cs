using System;
using System.IO;
using SQLite;
using Android.App;
using Graduate.Core;


namespace Graduate.Droid
{
    [Application]
    public class GraduateApp : Application
    {
        public static GraduateApp Current { get; private set; }

      public Calculator calculator { get; set; }


        SQLiteConnection conn;

        public GraduateApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {


            base.OnCreate();


            var stream = Android.App.Application.Context.Assets.Open("gpa.txt");


            var sqliteFilename = "Graduate.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new SQLiteConnection(path);

            calculator = new Calculator(conn, stream);

            

        }
    }
}

