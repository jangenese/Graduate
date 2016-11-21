using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Repository
{
   public class GradeRepository
    {
        private GradeDataAccess dataAccess;
        public GradeRepository(SQLiteConnection conn)
        {
            dataAccess = new GradeDataAccess(conn);
        }
        public void saveItem(Grade c)
        {
            dataAccess.SaveItem<Grade>(c);
        }
        public Grade getItemByPercent(int percent)
        {
            return dataAccess.getItemByPercent(percent);
        }
        public Grade getItemByLetter(String letter)
        {
            return dataAccess.getItemByLetter(letter);
        }
        public Grade getItemByGPA(double gpa)
        {
            return dataAccess.getItemByGPA(gpa);
        }
        public IEnumerable<Grade> getItems()
        {
            return dataAccess.GetItems<Grade>();
        }        
    }
}
