using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core.Data.Models
{
   public class Semester : GraduateEntityBase
    {
        public Semester() {
        }

        public Semester(String label) {
            this.label = label;
        }



        public int FId { get; set; } = 0;
        public String label { get; set; }

       
    }
}
