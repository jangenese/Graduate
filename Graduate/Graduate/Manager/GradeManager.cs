using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;
using Graduate.Core.MiscTools;

namespace Graduate.Core.Manager
{
   public class GradeManager
    {
        GradeDataAccess grades;
        public GradeManager(SQLiteConnection conn) {
            grades = new GradeDataAccess(conn);
        }

        public void initTable() {
            grades.initTable();

            GradePopulator gradesPopulator = new GradePopulator();
            IList<Grade> gradesRecords = gradesPopulator.getTableContents();
            foreach (Grade gradeEntry in gradesRecords)
            {
                grades.SaveItem(gradeEntry);
            }
        }

        private int stringToInt(String str) {

            int i = 999;

            try
            {
                i = Convert.ToInt32(str);
            }
            catch 
            {
                
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
                if (!labels.Contains(g.Letter))
                {
                    labels.Add(g.Letter);
                }
            }
            return labels;
        }

        public List<String> getPercentGrades()
        {
            List<String> labels = new List<String>();
            IEnumerable<Grade> grades = getAllGrades();

            foreach (Grade g in grades)
            {
                if (!labels.Contains(g.Percent.ToString())) {
                    labels.Add(g.Percent.ToString());
                }            
            }
            return labels;
        }

        public List<String> getGPAs()
        {
            List<String> labels = new List<String>();
            IEnumerable<Grade> grades = getAllGrades();

            foreach (Grade g in grades)
            {
                if (!labels.Contains(g.GPA.ToString()))
                {
                    labels.Add(g.GPA.ToString());
                }
            }
            return labels;
        }
    }
}
