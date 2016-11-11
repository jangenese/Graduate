using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Graduate.Core.Models;

namespace Graduate.Core.DAL.TableGateways
{
  public  class SemesterTableGateway
    {
        static object locker = new object();

        public SQLiteConnection database;

        public string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        public SemesterTableGateway(SQLiteConnection conn)
        {
            database = conn;
            // create the tables
            database.CreateTable<Semester>();
        }

        public IEnumerable<Semester> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<Semester>() select i).ToList();
            }
        }

        public Semester GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<Semester>().FirstOrDefault(x => x.ID == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
        }

        public int SaveItem(Semester item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<Semester>(id);
            }
        }
    }
}
