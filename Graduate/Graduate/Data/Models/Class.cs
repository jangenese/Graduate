using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.Data.Models
{
    public class Class : GraduateEntityBase
    {
        public Class() {
        }

        public int FId { get; set; }
        public String label { get; set; }
        public double grade { get; set; }
        public double goalGrade { get; set; }
        public Boolean completed { get; set; } = false;

        public int credits { get; set; }

        public Class(String label) {
            this.label = label;
        }

        public override string ToString()
        {
            return string.Format("[Class: ID={0}, Label={1}], GoalGrade={2}, Grade={3}, Completed={4}, Credits={5}]", Id, label, goalGrade, grade, completed, credits);
        }


    }
}
