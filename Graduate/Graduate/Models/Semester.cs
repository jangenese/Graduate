using System;
using SQLite;

namespace Graduate.Core.Models
{
    public class Semester
    {

        // SQLite attributes
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
       
        public string Label { get; set; }
        
        public override string ToString()
        {
            return string.Format("[Grade: ID={0}, Label={1}]", ID, Label);
        }

        public Semester()
        {
        }

        public Semester(String Label)
        {
            this.Label = Label;
        }

    }
}
