﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.MiscTools;
using Graduate.Core.Data.Models;

namespace Graduate.Core.Data.DataAccessLayer
{
    public class GradeDataAccess : GraduateDatabase
    {
        private static object collisionLock = new object();
        SQLiteConnection database;

        public GradeDataAccess(SQLiteConnection conn) : base(conn)
        {
            database = conn;            
        }


        public void initTable() {
            try { database.DropTable<Grade>(); }catch { }

            database.CreateTable<Grade>();
        }    

        public Grade getItemByPercent(int percent)
        {
            lock (collisionLock)
            {
                return database.Table<Grade>().
                  FirstOrDefault(grade => grade.Percent == percent);
            }
        }

        public Grade getItemByLetter(String letter)
        {
            lock (collisionLock)
            {
                return database.Table<Grade>().
                  FirstOrDefault(grade => grade.Letter == letter);
            }
        }

        public Grade getItemByGPA(double gpa)
        {
            lock (collisionLock)
            {
                return database.Table<Grade>().
                  FirstOrDefault(grade => grade.GPA == gpa);
            }
        }
    }
}
