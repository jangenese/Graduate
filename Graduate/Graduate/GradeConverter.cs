using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;

namespace Graduate.Core
{
   public class GradeConverter
    {
        GradeManager gradeManager;
        public GradeConverter(SQLiteConnection conn) {
            gradeManager = new GradeManager(conn);
        }

        public void initGradeSchema() {
            gradeManager.initTable();
        }

        public Grade convertPercent(String percent) {
           return gradeManager.getByPercent(percent);
        }

        public Grade convertLetter(String letter) {
            return gradeManager.getByLetter(letter);
        }

        public String ToStringGrades() {
            String str = "";
           IEnumerable<Grade> grades = gradeManager.getAllGrades();
            foreach (Grade grade in grades) {
                str += grade.ToString();

            }

            return str;
        }

        public double estimateGPA(String strCurrentGPA, String strRemainingCredits, String strRequireCredits, String strDdesiredGPA) {
                                                                                           

            decimal currentGPA = Convert.ToDecimal(strCurrentGPA);                                                                                           

            decimal remainingCredits = Convert.ToDecimal(strRemainingCredits);
                                                                                            
            decimal requiredCredits = Convert.ToDecimal(strRequireCredits);                                                                                             

            decimal desiredGPA = Convert.ToDecimal(strDdesiredGPA);

            

            decimal totalDesiredPoints = requiredCredits * desiredGPA;

            decimal earnedPoint = (requiredCredits - remainingCredits) * currentGPA;

            decimal missingPoints = totalDesiredPoints - earnedPoint;

            decimal result = missingPoints / remainingCredits;


            return Convert.ToDouble(result);
        }
    }

}
