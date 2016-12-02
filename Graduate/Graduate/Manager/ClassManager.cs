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
            c.goalGrade = 4.00;
            c.grade = getGradeFromSchema(grade);
            c.credits = stringToInt(credit);
            c.completed = completed;
            repo.saveItem(c);
        }

        public void UpdateItem(String id, String fid, String label, String grade, String credit, Boolean completed) {
            Class c = new Class();
            c.Id = stringToInt(id);
            c.FId = stringToInt(fid);
            c.label = label;
            c.goalGrade = 4.00;
            c.grade = getGradeFromSchema(grade);
            c.credits = stringToInt(credit);
            c.completed = completed;
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
            Class c = repo.getItem(stringToInt(id));
            repo.deleteItem(c);
        }

        private int stringToInt(String str)
        {
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

        private double stringToDouble(String str)
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(str);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return i;
        }

        private double getGradeFromSchema(String letter) {
            

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
    }
}
