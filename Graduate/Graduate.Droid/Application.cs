using System;
using System.IO;

using Android.App;


namespace Graduate.Droid
{
    [Application]
    public class GraduateApp : Application
    {
        public static GraduateApp Current { get; private set; }

    //    public Graduate.core.Calculator Calculator { get; set; }
       

        String conn;

        public GraduateApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {


            base.OnCreate();

           
            conn = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGraduate.db3");

         //   Calculator = new Graduate.core.Calculator(conn);

        }
    }
}

