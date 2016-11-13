using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using Graduate.Core.Data.DataAccessLayer;
using SQLite;

namespace Graduate.Core
{
   public class GradeConverter
    {
        GradeDataAccess grades;
        public GradeConverter(SQLiteConnection conn) {
            grades = new GradeDataAccess(conn);
        }



        private int stringToInt(String str) {
            int i = 0;

            try {
                i = Convert.ToInt32(str);
            }
            catch (System.Exception e) {
            }

            return i;
        }

        private Grade searchByPercent(int percent) {
            return grades.getItemByPercent(percent);
        }


        public Grade convertFromPercent(String str) {
            int i = stringToInt(str);
            Grade grade = grades.getItemByPercent(i);

            if (isNull(grade)) {
                grade = returnNullGrade();
            }

            return grade;
        }

        private Boolean isNull(Grade grade) {
            Boolean b = false;

            if (grade == null) {
                b = true;
            }

            return b;
        }

        private Grade returnNullGrade() {
            return new Grade();
        }
    }
}
