using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Graduate.Core.Data.Models;

namespace Graduate.Core.MiscTools
{
    class GradePopulator
    {
       
        public GradePopulator()
        {
            
        }

        public IList<Grade> getTableContents()
        {

            IList<Grade> grades = new List<Grade>();          

            String[] custom = new String[12];
            custom[0] = "95\t100\tA+\t4.00\t";
            custom[1] = "85\t94\tA\t4.00\t";
            custom[2] = "80\t84\tA-\t3.70\t";
            custom[3] = "77\t79\tB+\t3.30\t";
            custom[4] = "73\t76\tB\t3.00\t";
            custom[5] = "70\t72\tB-\t2.70\t";
            custom[6] = "67\t69\tC+\t2.30\t";
            custom[7] = "63\t66\tC\t2.00\t";
            custom[8] = "60\t62\tC-\t1.70\t";
            custom[9] = "55\t59\tD+\t1.30\t";
            custom[10] = "50\t54\tD\t1.00\t";
            custom[11] = "0\t49\tF\t0.00\t";


            foreach (String line in custom) {
                String[] part = line.Split('\t');

                int min = Convert.ToInt32(part[0].ToString());
                int max = Convert.ToInt32(part[1]);
                String letter = part[2];
                double gpa = Convert.ToDouble(part[3]);


                for (int i = min; i <= max; i++) {
                    Grade grade = new Grade(i, letter, gpa);

                    grades.Add(grade);
                }

            }

            return grades;
        }
    }
}
