using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;

namespace DataLayer
{
    public class QueryExecutor
    {
        private readonly string ConnString = @"Data Source=C:\Users\Przemek\source\repos\TV_App\TV_App\DataLayer\test.sqlite;Version=3;";
        private SQLiteConnection Conn { get { return new SQLiteConnection(ConnString); } }

        public IEnumerable<T> Query<T>(string query, IDictionary<string, object> parameters = null)
        {   
            
            return Conn.Query<T>(query);
        }
        

    }
}
