using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.IO;

namespace DataLayer
{
    public class QueryExecutor
    {
        const string PROJECT_DIRECTORY_NAME = "TV_App";
        const string DATABASE_FILE = "test.sqlite";

        private readonly string ConnString = $"Data Source={SQLiteFileLocation()};Version=3;";
        private SQLiteConnection Conn { get { return new SQLiteConnection(ConnString); } }

        public IEnumerable<T> Query<T>(string query, IDictionary<string, object> parameters = null)
        {   
            
            return Conn.Query<T>(query);
        }




        private static string SQLiteFileLocation()
        {
            string path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            if (path.Contains(PROJECT_DIRECTORY_NAME))
            {
                path = Path.Combine(path.Substring(0, path.LastIndexOf(PROJECT_DIRECTORY_NAME)), PROJECT_DIRECTORY_NAME, "DataLayer", DATABASE_FILE);
                return path;
            }
            else throw new DirectoryNotFoundException($"directory {PROJECT_DIRECTORY_NAME} not found in path {path}");

        }
    }
}
