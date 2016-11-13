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

       
        String testReturn;
        GradeDataAccess gradesGateway;
        String testContent;
        public Calculator(SQLiteConnection conn) {
            gradesGateway = new GradeDataAccess(conn);            

            IList<Grade> grades = new List<Grade>(gradesGateway.GetItems<Grade>());
            foreach (Grade i in grades)            {
                testContent += i.ToString() + "\n";
            }
            testPercentLookup(79);
        }
        

        public void testPercentLookup(int percent) {
            Grade gradeResult = gradesGateway.getItemByPercent(percent);
            testReturn = gradeResult.ToString();
        }

        public String getTestReturn() {
            return testReturn;
        }

        public String toStringContent()
        {
            return testContent;
        }


        public Grade getPercent(String percent) {



            int percentLookup = 100;

          

            try {
                percentLookup = Convert.ToInt32(percent);
            }catch (Exception e) {
               
            }

           

            Grade grade = gradesGateway.getItemByPercent(95);
            /*  

              try
              {
                  grade.GPA.ToString();
              }
              catch (Exception e)
              {
                  grade = new Grade();
                  grade.GPA = 0;
                  grade.Letter = "";
              }

      */


            if (grade == null) {
                grade = new Grade();
                grade.GPA = 0;
                grade.Letter = "null recieved";
            }

            return grade;

        }


        
    }
    
}
