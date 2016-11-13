using System;
using System.IO;
using System.Collections.Generic;
using SQLite;
using Graduate.Core.Data.Models;
using Graduate.Core.Data.DataAccessLayer;


namespace Graduate.Core
{
   public class Calculator
    {
        GradeDataAccess gradesGateway;
        String testContent;
        public Calculator(SQLiteConnection conn, Stream fileStream) {
            gradesGateway = new GradeDataAccess(conn);


            

            IList<Grade> grades = new List<Grade>(gradesGateway.GetItems<Grade>());

            foreach (Grade i in grades)
            {
                testContent += i.ToString() + "\n";
            }
        }


        public String toStringContent()
        {
            return testContent;
        }


        public Grade getPercent(String percent) {

            int percentLookup;

            try {
                percentLookup = Int32.Parse(percent);
            }catch (Exception e) {
                percentLookup = 0;
            }
            

            Grade grade =  gradesGateway.getItemByPercent(percentLookup);

            try {
                grade.GPA.ToString();
            } catch (Exception e) {
                grade = new Grade();
                grade.GPA = 0;
                grade.Letter = "";
            }

            return grade;

        }
    }
    
}
