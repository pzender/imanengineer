using Logic.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicTests
{
    public class FakeConnectionRepository<T, T1, T2> : IRepository<T> where T : IConnection<T1, T2>
    {
        public List<T> content = new List<T>();

        public int Count()
        {
            return content.Count;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Func<T, bool> filter)
        {
            return content.Where(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return content;
        }

        public IEnumerable<T> Insert(T value)
        {
            content.Add(value);
            return content;
        }

        public IEnumerable<T> InsertAll(IEnumerable<T> values)
        {
            foreach (T v in values)
                Insert(v);
            return content;
        }

        public void Replace(int id, T value)
        {
            throw new NotImplementedException();
        }


        //public IEnumerable<IConnection<T1, T2>> Insert(IConnection<T1, T2> value)
        //{
        //    value.id = content.Select(t => t.id).DefaultIfEmpty().Max() + 1;
        //    content.Add(value);
        //    return content;
        //}

        //public IEnumerable<IConnection<T1, T2>> InsertAll(IEnumerable<IConnection<T1, T2>> values)
        //{
        //    foreach (IConnection<T1, T2> v in values)
        //        Insert(v);
        //    return content;
        //}

        //public void Replace(int id, IConnection<T1, T2> value)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
