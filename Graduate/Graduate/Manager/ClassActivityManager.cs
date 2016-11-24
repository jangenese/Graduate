using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Graduate.Core.Repository;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Manager
{
   public class ClassActivityManager
    {

        ClassActivityRepository repo;
        public ClassActivityManager(SQLiteConnection conn)
        {
            repo = new ClassActivityRepository(conn);
        }

        public void SaveItem(String fid, String label, String grade, String weight, Boolean completed)
        {
            ClassActivity activity = new ClassActivity();
            activity.FId = stringToInt(fid);
            activity.label = label;
            activity.goalGrade = 4.00;
            activity.grade = stringToDouble(grade);
            activity.weight = stringToInt(weight);
            activity.completed = completed;
            repo.saveItem(activity);
        }

        public ClassActivity getClassActivityByID(String id)
        {
            int i = stringToInt(id);
            ClassActivity c = repo.getItem(i);

            if (isNull(c))
            {
                c = returnNullEntity();
            }

            return c;
        }

        public IEnumerable<ClassActivity> getClassActivities()
        {
            return repo.getItems();
        }

        public IEnumerable<ClassActivity> getClassActivitiesByFID(String fid)
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

        private Boolean isNull(ClassActivity entity)
        {
            Boolean b = false;

            if (entity == null)
            {
                b = true;
            }

            return b;
        }

        private ClassActivity returnNullEntity()
        {
            return new ClassActivity();
        }
    }
}
