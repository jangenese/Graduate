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
        public double gpaGoalGrade { get; set; }
        public int percentGoalGrade { get; set; }
        public String letterGoalGrade { get; set;}
        public int percentGrade { get; set; }
        public double gpaGrade { get; set; }
        public string letterGrade { get; set; }       
        public Boolean completed { get; set; } = false;
        public double credits { get; set; }
       

        public override string ToString()
        {
            return string.Format("[Class: ID={0}, Label={1}], GoalPercentGrade={2},GoalGPAGrade={3}, GoalLetterGrade={4}, PercentGrade={5}, LetterGrade={7}, GPAGrade={6}, Completed={8}, Credits={9}]", 
                                            Id, label, percentGoalGrade,gpaGoalGrade,letterGoalGrade, percentGrade, letterGrade, gpaGrade, completed, credits);
        }


    }
}
