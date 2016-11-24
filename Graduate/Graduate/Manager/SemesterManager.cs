using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Graduate.Core.Data.Models;
using Graduate.Core.Repository;
using SQLite;

namespace Graduate.Core.Manager
{
    public class SemesterManager
    {
        SemesterRepository repo;
        public SemesterManager(SQLiteConnection conn)
        {
            repo = new SemesterRepository(conn);
        }

        public void SaveItem(String fid, String label)
        {
            Semester sem = new Semester();
            sem.FId = stringToInt(fid);
            sem.label = label;   

            repo.saveItem(sem);
        }

        public IEnumerable<Semester> getSemesters()
        {
            return repo.getItems();
        }

        public Semester getSemesterByID(String id)
        {
            int i = stringToInt(id);
            Semester sem = repo.getItem(i);

            if (isNull(sem))
            {
                sem = returnNullEntity();
            }

            return sem;
        }

        public IEnumerable<Semester> getSemestersByFID(String fid)
        {
            int i = stringToInt(fid);
            return repo.getItemsByFID(i);
        }
        private int stringToInt(String str)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(str);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return i;
        }

        private double stringToDouble(String str)
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(str);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return i;
        }

        private Boolean isNull(Semester entity)
        {
            Boolean b = false;

            if (entity == null)
            {
                b = true;
            }

            return b;
        }

        private Semester returnNullEntity()
        {
            return new Semester();
        }

        public List<String> getSemesterLabels()
        {
            List<String> labels = new List<String>();
            IEnumerable<Semester> semesters = getSemesters();

            foreach (Semester sem in semesters)
            {
                labels.Add(sem.label.ToString());
            }

            return labels;
        }

    }
}
