using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Manager;
using Graduate.Core.Data.Models;
using Graduate.Core.View.Model;

namespace Graduate.Core.View.Manager
{
    public class SchoolYearViewManager
    {
        SchoolYearManager schoolYearManager;
        SemesterManager semesterManager;
        ClassManager classManager;
        public SchoolYearViewManager(SQLiteConnection conn) {
            schoolYearManager = new SchoolYearManager(conn);
            semesterManager = new SemesterManager(conn);
            classManager = new ClassManager(conn);
        }

        public SchoolYearView getSchoolYearView(String id) {
            SchoolYear sy = schoolYearManager.getSchoolYearByID(id);

            return populateSchoolView(sy);
        }

        private IEnumerable<Semester> getChildren(String fid) {
            return semesterManager.getSemestersByFID(fid);
        }

        private SchoolYearView populateSchoolView(SchoolYear sy) {
            SchoolYearView syView = new SchoolYearView();
            syView.id = sy.Id;
            syView.label = sy.label;
            syView.children = getChildren("1");            
            return syView;
        }

        private int getCreditsFromChildren() {
            return 0;
        }

        private double getGradeFromChildren()
        {
            return 0.00;
        }
    }
}
