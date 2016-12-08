using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.Data.Models
{
  public  class Grade : GraduateEntityBase
    {
        public int Percent { get; set; } = 0;
        public string Letter { get; set; } = "";
        public double GPA { get; set; } = 0.0;
        public override string ToString()
        {
            return string.Format("[Grade: ID={0}, Percent={1}, Letter={2}, GPA={3}]", Id, Percent, Letter, GPA);
        }

        public Grade()
        {
        }

        public Grade(int percent, String letter, Double gpa)
        {
            Percent = percent;
            Letter = letter;
            GPA = gpa;
        }

    }
}
