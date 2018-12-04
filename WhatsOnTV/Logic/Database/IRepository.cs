using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database
{
    public interface IRepository<T>
    {
        

        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> filter);
        T GetByID(int id);

        IEnumerable<T> Insert(T value);
        IEnumerable<T> InsertAll(IEnumerable<T> values);

        void Replace(int id, T value);
        void Delete(int id);

        int Count();
    }
}
