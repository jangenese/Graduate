using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;
using Graduate.Core.Manager;
using SQLite;

namespace Graduate.Core.View.Manager
{
   public class ClassViewManager
    {
        SchoolYearManager schoolYearManager;
        ClassManager classesterManager;
        ClassManager classManager;
        public ClassViewManager(SQLiteConnection conn)
        {
            schoolYearManager = new SchoolYearManager(conn);
            classesterManager = new ClassManager(conn);
            classManager = new ClassManager(conn);
        }

        public ClassView getClassView(String id)
        {
            Class c = classManager.getClassByID(id);

            return populateClassView(c);
        }
             
        private ClassView populateClassView(Class c)
        {
            ClassView classView = new ClassView();
            classView.id = c.Id;
            classView.label = c.label;
            
            return classView;
        }

        private int getCreditsFromChildren()
        {
            return 0;
        }

        private double getGradeFromChildren()
        {
            return 0.00;
        }

    }
}
