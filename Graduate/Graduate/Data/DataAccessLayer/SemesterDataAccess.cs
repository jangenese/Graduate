using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
  public  class SemesterDataAccess : GraduateDatabase
    {
        private static object collisionLock = new object();

        SQLiteConnection database;

        public SemesterDataAccess(SQLiteConnection conn) : base(conn) {
            database = conn;
        }



        public Semester getItemById(int id)
        {


            lock (collisionLock)
            {
                return database.Table<Semester>().
                  FirstOrDefault(sem => sem.Id == id);
            }


        }
    }
}
