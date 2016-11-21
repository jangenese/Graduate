using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.View.Model;
using SQLite;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;

namespace Graduate.Core.View.Manager
{
  public  class SemesterViewManager
    {     

        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        public SemesterViewManager(SQLiteConnection conn)
        {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
        }

        public SemesterView getSemesterView(String id)
        {
            Semester sem = semesterManager.getSemesterByID(id);

            return populateSemesterView(sem);
        }

        private IList<Class> getChildren(String fid)
        {
            return classManager.getClasssByFID(fid).ToList<Class>();
        }

        private SemesterView populateSemesterView(Semester sem)
        {
            SemesterView semesterView = new SemesterView();
            semesterView.id = sem.Id;
            semesterView.label = sem.label;
            semesterView.children = getChildren("1");
            return semesterView;
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
