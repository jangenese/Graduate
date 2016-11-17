using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.Data.Models
{
    public class SchoolYear : GraduateEntityBase
    {
        public SchoolYear() { }

        
        public String label { get; set; }
        public int grade { get; set; }
        public int goalGrade { get; set; }


        public SchoolYear(String label) {
            this.label = label;

        }

       
    }
}
