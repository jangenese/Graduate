using System;
using SQLite;

namespace Graduate.Core.Models
{
    public class Grade
    {

        // SQLite attributes
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Percent { get; set; }
        public string Letter { get; set; }
        public double GPA { get; set; }
        public override string ToString()
        {
            return string.Format("[Grade: ID={0}, Percent={1}, Letter={2}, GPA={3}]", ID, Percent, Letter, GPA);
        }

        public Grade()
        {
        }

        public Grade(int percent, String letter, Double gpa) {
            Percent = percent;
            Letter = letter;
            GPA = gpa;
        }

    }
}
