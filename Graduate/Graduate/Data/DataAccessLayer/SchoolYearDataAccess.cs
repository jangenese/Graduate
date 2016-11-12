using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core.Data.DataAccessLayer
{
   public class SchoolYearDataAccess : GraduateDatabase
    {
        public SchoolYearDataAccess(SQLiteConnection conn) : base(conn) {
        }
    }
}
