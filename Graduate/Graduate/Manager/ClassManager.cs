using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Graduate.Core.Data.DataAccessLayer;
using Graduate.Core.Data.Models;

using SQLite;

namespace Graduate.Core.Manager
{
    public class ClassManager
    {

        ClassDataAccess classs;
        public ClassManager(SQLiteConnection conn)
        {
            classs = new ClassDataAccess(conn);
        }


        public IEnumerable<Class> getAll()
        {
            return classs.GetItems<Class>();
        }

        public void addItem(Class sem)
        {
            classs.SaveItem<Class>(sem);
        }

        public String toStringAllClasss()
        {
            String str = "";

            IList<Class> sems = classs.GetItems<Class>().ToList<Class>();
            foreach (Class sem in sems)
            {
                str += sem.ToString() + "\n";
            }

            return str;
        }

        public Class getClass(int id)
        {
             return classs.getItemById(id);

          //  return classs.GetItem<Class>(id); //borken

        }

        public IEnumerable<Class> getClassesByFID(int fid) {
            return classs.getItemsByFID(fid);
        }

        
    }
}
