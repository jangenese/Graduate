using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core.Data.DataAccessLayer
{
  public  class SemesterDataAccess : GraduateDatabase
    {

        public SemesterDataAccess(SQLiteConnection conn) : base(conn) {
            
        }

        public override string ToString()
        {
            return "SemesterDataAccess";
        }
    }
}
