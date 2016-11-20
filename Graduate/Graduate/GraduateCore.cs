using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core
{
  public  class GraduateCore
    {
        public Planner planner { get; }
        public GradeConverter converter { get; }
        public GraduateCore(SQLiteConnection conn) {
            planner = new Planner(conn);
            converter = new GradeConverter(conn);
        }

        public void initCore() {

        }
    }
}
