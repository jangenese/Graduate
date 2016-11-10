using System;
using SQLite;

namespace Graduate.Core.DAL
{
    public class SQLiteDB
    {
        public SQLiteConnection DbConnection(String conn)
        {
            return new SQLiteConnection(conn);
        }
    }
}
