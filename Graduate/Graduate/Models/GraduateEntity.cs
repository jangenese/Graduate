using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Graduate.Core.Models
{
   public class GraduateEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public int FID { get; set; }
        public Grade gade { get; set; }
        public String label { get; set; }
        public int credits { get; set; }
        public GraduateEntity childEntity { get; set; }
        

        public GraduateEntity() {
        }
    }
}
