using System;
using System.IO;
using Graduate.Core.DAL.TableGateways;
using System.Collections.Generic;
using SQLite;
using Graduate.Core.Models;

namespace Graduate.Core
{
   public class Calculator
    {
        GradesTableGateway gradesGateway;
        String testContent;
        public Calculator(SQLiteConnection conn, Stream fileStream) {
            gradesGateway = new DAL.TableGateways.GradesTableGateway(conn, fileStream);

           

        //    for (int i = 0; i < 50; i++)
      //      {
      //          Grade g = new Grade();
      //          g.GPA = 4.00;
      //          g.Letter = "A+";
     //           g.Percent = 100;
    //            gradesGateway.SaveItem(g);
   //         }

            IList<Grade> grades = new List<Grade>(gradesGateway.GetItems());

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

            int percentLookup = Int32.Parse(percent);

            return gradesGateway.getItemByPercent(percentLookup);

        }
    }
    
}
