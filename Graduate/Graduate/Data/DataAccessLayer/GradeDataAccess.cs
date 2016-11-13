using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.MiscTools;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class GradeDataAccess : GraduateDatabase
    {

        SQLiteConnection database;

        public GradeDataAccess(SQLiteConnection conn) : base(conn)
        {

            database = conn;
        }


        public void init()
        {

            GradePopulator grades = new GradePopulator();
            IList<Grade> gradesRecords = grades.getTableContents();

            try
            {



                database.DropTable<Grade>();
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new System.IO.FileNotFoundException("Table not Found", e);
            }


            database.CreateTable<Grade>();


            foreach (Grade gradeEntry in gradesRecords)
            {

                SaveItem(gradeEntry);
            }

        }



        public Grade getItemByPercent(int percent)
        {



            return database.Table<Grade>().FirstOrDefault(x => x.Percent == percent);


        }
    }
}
