using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Manager
{
   public class GradeManager
    {
        GradeDataAccess grades;
        public GradeManager(SQLiteConnection conn) {
            grades = new GradeDataAccess(conn);
        }

        private int stringToInt(String str) {

            int i = 0;

            try
            {
                i = Convert.ToInt32(str);
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return i;

        }

        public IEnumerable<Grade> getAllGrades() {
           
            IEnumerable<Grade> enumerable = grades.GetItems<Grade>();
            List<Grade> gradesList = enumerable.ToList<Grade>();

            return gradesList;
        }

        public Grade getByPercent(String str) {
            int i = stringToInt(str);
            Grade grade = grades.getItemByPercent(i);

           if (isNull(grade))
           {
                grade = returnNullGrade();
           }

            return grade;
        }
        public Grade getByLetter(String str) {

            Grade grade = grades.getItemByLetter(str);

            if (isNull(grade))
            {
                grade = returnNullGrade();
            }

            return grade;
        }
        public void getByGPA() { }

        private Boolean isNull(Grade grade)
        {
            Boolean b = false;

            if (grade == null)
            {
                b = true;
            }

            return b;
        }

        private Grade returnNullGrade()
        {
            return new Grade();
        }


        public List<String> getLetterGrades()
        {
            List<String> labels = new List<String>();
            IEnumerable<Grade> grades = getAllGrades();

            foreach (Grade g in grades)
            {
                labels.Add(g.Letter);
            }
            return labels;
        }

        public List<String> getPercentGrades()
        {
            List<String> labels = new List<String>();
            IEnumerable<Grade> grades = getAllGrades();

            foreach (Grade g in grades)
            {
                labels.Add(g.Percent.ToString());
            }
            return labels;
        }

        public List<String> getGPAs()
        {
            List<String> labels = new List<String>();
            IEnumerable<Grade> grades = getAllGrades();

            foreach (Grade g in grades)
            {
                labels.Add(g.GPA.ToString());
            }
            return labels;
        }
    }
}
