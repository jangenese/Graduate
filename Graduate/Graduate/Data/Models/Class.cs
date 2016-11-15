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

        public Class(String label) {
            this.label = label;
        }


    }
}
