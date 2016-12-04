using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.Data.Models
{
    public class ClassActivity : GraduateEntityBase
    {
        public int FId { get; set; }
        public String label { get; set; }        
        public int grade { get; set; }        
        public Boolean completed { get; set; } = false;
        public int weight { get; set; }

        public ClassActivity() {
        }

        public ClassActivity(int fid, String label, int weight, int mark, Boolean completed) {

        }

        public override string ToString()
        {
            return string.Format("[ClassActivity: ID={0}, Label={1}], Grade={2}, Completed={3}, Weight={4}]", Id, label, grade, completed, weight);
        }
    }
}
