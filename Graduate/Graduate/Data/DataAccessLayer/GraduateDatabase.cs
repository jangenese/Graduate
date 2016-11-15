using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;
using SQLite;

namespace Graduate.Core.Data.DataAccessLayer
{
  public class GraduateDatabase
    {
        SQLiteConnection connection { get; }

        private static object collisionLock = new object();

        public GraduateDatabase(SQLiteConnection conn)
        {
            this.connection = conn;

          
            

            connection.CreateTable<SchoolYear>();
            connection.CreateTable<Semester>();
            connection.CreateTable<Class>();
            connection.CreateTable<Grade>();

        }


        public T GetItem<T>(int id) where T : IGraduateEntity, new()
        {
            lock (collisionLock)
            {
                return connection.Table<T>().
                  FirstOrDefault(i => i.Id == id);
            }

           // return (from i in connection.Table<T>()
           //         where i.Id == id
            //        select i).FirstOrDefault();


        }

        public IEnumerable<T> GetItems<T>() where T : IGraduateEntity, new()
        {

            return (from i in connection.Table<T>()
                    select i);

        }

        public int SaveItem<T>(T item) where T : IGraduateEntity
        {

            if (item.Id != 0)
            {
                connection.Update(item);
                return item.Id;
            }

            return connection.Insert(item);

        }

        public void SaveItems<T>(IEnumerable<T> items) where T : IGraduateEntity
        {

            connection.BeginTransaction();

            foreach (T item in items)
            {
                SaveItem(item);
            }

            connection.Commit();

        }

        public int DeleteItem<T>(T item) where T : IGraduateEntity, new()
        {

            return connection.Delete(item);

        }

    }
}
