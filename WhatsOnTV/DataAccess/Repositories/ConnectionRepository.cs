using Dapper;
using Logic.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class ConnectionRepository<T, T1, T2> : IRepository<T> where T : IConnection<T1, T2>
    {
        private readonly string connectionString = "Server=tcp:whatsontv-db.database.windows.net,1433;Initial Catalog=whatsontv-db;Persist Security Info=False;User ID=TvApplication;Password=Tv@pplicati0n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
        private readonly SQLBuilder<T> sqlBuilder = new SQLBuilder<T>();

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Func<T, bool> filter)
        {
            //slow AF?
            List<T> candidates = Connection.Query<T>(sqlBuilder.BuildSelect()).ToList();
            return candidates.Where(filter);
            //throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return Connection.Query<T>(sqlBuilder.BuildSelect());
        }

        public T GetByID(int id)
        {
            return Connection.Query<T>(sqlBuilder.BuildSelect(where: new Dictionary<string, string>() { { "id", $"{id}" } })).Single();
        }

        public IEnumerable<T> Insert(T value)
        {
            Connection.Execute(sqlBuilder.BuildInsert(new List<T>() { value }));
            return Connection.Query<T>(sqlBuilder.BuildSelect());
        }

        public IEnumerable<T> InsertAll(IEnumerable<T> values)
        {
            Connection.Execute(sqlBuilder.BuildInsert(values));
            return Connection.Query<T>(sqlBuilder.BuildSelect());
        }

        public void Replace(int id, T value)
        {
            throw new NotImplementedException();
        }
    }
}
