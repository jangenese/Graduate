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


        public Grade convertPercent(String percent) {
           return gradeManager.getByPercent(percent);
        }

        public Grade convertLetter(String letter) {
            return gradeManager.getByLetter(letter);

        }
    }
}
