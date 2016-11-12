using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class ClassDataAccess : GraduateDatabase
    {
        public ClassDataAccess(SQLiteConnection conn) : base(conn) {
        }
    }
}
