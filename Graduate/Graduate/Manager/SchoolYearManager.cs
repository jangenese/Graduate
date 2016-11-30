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
    public class SchoolYearManager
    {
        SchoolYearRepository repo;
        public SchoolYearManager(SQLiteConnection conn)
        {
            repo = new SchoolYearRepository(conn);
        }

        public void SaveItem(String label)
        {
            SchoolYear sy = new SchoolYear();           
            sy.label = label;   
            repo.saveItem(sy);
        }

        public void UpdateItem(String id, String label) {
            SchoolYear sy = new SchoolYear();
            sy.Id = stringToInt(id);
            sy.label = label;
            repo.saveItem(sy);
        }
        public IEnumerable<SchoolYear> getSchoolYears()
        {
            return repo.getItems();
        }

        public SchoolYear getSchoolYearByID(String id)
        {
            int i = stringToInt(id);
            SchoolYear sy = repo.getItem(i);

            if (isNull(sy)) {
                sy = returnNullEntity();
            }

            return sy;
        }


        public void deleteSchoolYear(String id)
        {
            SchoolYear sy = repo.getItem(stringToInt(id));
            repo.deleteItem(sy);
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

        private Boolean isNull(SchoolYear entity)
        {
            Boolean b = false;

            if (entity == null)
            {
                b = true;
            }

            return b;
        }

        private SchoolYear returnNullEntity()
        {
            return new SchoolYear();
        }

        public List<String> getSchoolYearLabels()
        {
            List<String> labels = new List<String>();
            IEnumerable <SchoolYear> schoolYears= getSchoolYears();

            foreach (SchoolYear sy in schoolYears) {
                labels.Add(sy.label.ToString());
            }

            return labels;
        }

    }
}
