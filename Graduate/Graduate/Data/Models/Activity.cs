using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.Data.Models
{
    public class Activity : GraduateEntityBase
    {
        public int FId { get; set; }
        public String label { get; set; }
        public double grade { get; set; }
        public double goalGrade { get; set; }
        public Boolean completed { get; set; } = false;
        public int weight { get; set; }

        public Activity() {
        }

        public Activity(int fid, String label, int weight, int mark, Boolean completed) {

        }
    }
}
