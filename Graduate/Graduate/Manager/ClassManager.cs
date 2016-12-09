using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.Repository;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Manager
{
    public class ClassManager
    {
        ClassRepository repo;
        GradeRepository gradeRepo;
        public ClassManager(SQLiteConnection conn) {
            repo = new ClassRepository(conn);
            gradeRepo = new GradeRepository(conn);
        }

        public void SaveItem(String fid, String label, String grade, String credit, Boolean completed) {
            Class c = new Class();           
            c.FId = stringToInt(fid);
            c.label = label;
            c.credits = stringToDouble(credit);
            c.completed = completed;

            //user entered grade is either completed or goal in letter form
                     
                c.letterGrade = grade;
                c.percentGrade = getPercentGradeFromSchema(grade);
                c.gpaGrade = getGPAGradeFromSchema(grade);

                c.percentGoalGrade = getPercentGradeFromSchema(grade);
                c.letterGoalGrade = grade;
                c.gpaGoalGrade = getGPAGradeFromSchema(grade);
            
          
            repo.saveItem(c);
        }

        public void UpdateItem(String id, String fid, String label, String grade, String credit, Boolean completed) {
            Class c = new Class();
            c.Id = stringToInt(id);
            c.FId = stringToInt(fid);
            c.label = label;
            c.credits = stringToDouble(credit);
            c.completed = completed;

            //user entered grade is either completed or goal in letter form

            if (completed)
            {
                c.letterGrade = grade;
                c.percentGrade = getPercentGradeFromSchema(grade);
                c.gpaGrade = getGPAGradeFromSchema(grade);

                c.percentGoalGrade = getPercentGradeFromSchema(grade);
                c.letterGoalGrade = grade;
                c.gpaGoalGrade = getGPAGradeFromSchema(grade);
            }
            else
            {
                c.percentGoalGrade = getPercentGradeFromSchema(grade);
                c.letterGoalGrade = grade;
                c.gpaGoalGrade = getGPAGradeFromSchema(grade);
            }

            repo.saveItem(c);
        }

        public Class getClassByID(String id) {
            int i = stringToInt(id);
            Class c = repo.getItem(i);

            if (isNull(c)) {
                c = returnNullEntity();
            }

            return c;
        }

        public IEnumerable<Class> getClasses() {
            return repo.getItems();
        }

        public IEnumerable<Class> getClasssByFID(String fid) {
            int i = stringToInt(fid);
            return repo.getItemsByFID(i);
        }

        public void deleteClass(String id) {
            System.Diagnostics.Debug.WriteLine("In Class Manager for Delete");

            Class c = repo.getItem(stringToInt(id));

            System.Diagnostics.Debug.WriteLine("Class to be Deleted is" + c.ToString());
            repo.deleteItem(stringToInt(id));

            
        }


        public void SaveItem(Class c) {
            repo.saveItem(c);
        }

        private int stringToInt(String str)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(str);
            }
            catch
            {
                
            }
            return i;
        }

        

        private double stringToDouble(String str)
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(str);
            }
            catch
            {
                
            }
            return i;
        }

        private int getPercentGradeFromSchema(String letter) {
            

            Grade g = gradeRepo.getItemByLetter(letter);

            return g.Percent;          
        }

        private double getGPAGradeFromSchema(String letter)
        {


            Grade g = gradeRepo.getItemByLetter(letter);

            return g.GPA;
        }

        private Boolean isNull(Class entity)
        {
            Boolean b = false;

            if (entity == null)
            {
                b = true;
            }

            return b;
        }

        private Class returnNullEntity()
        {
            return new Class();
        }


        

        private String getLetterFromSchema(int percent) {
            return gradeRepo.getItemByPercent(percent).Letter;
        }   


    }
}
