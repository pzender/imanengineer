using Logic.Database;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicTests
{
    public class FakeRepository<T> : IRepository<T> where T : AbstractEntity
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
            throw new NotImplementedException();
        }

        public T GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Insert(T value)
        {
            value.id = content.Select(t => t.id).DefaultIfEmpty().Max() + 1;
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
            int index_to_replace = content.FindIndex(t => t.id == id);
            content[index_to_replace] = value;
        }
    }
}
