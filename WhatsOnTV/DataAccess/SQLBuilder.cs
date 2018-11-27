using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class SQLBuilder<T>
    {
        public string BuildInsert(IEnumerable<T> values)
        {
            string query = $"INSERT INTO {typeof(T).Name} (";
            foreach(var attr in typeof(T).GetProperties())
            {
                if (attr.Name == "id") continue;
                query += $"{attr.Name}, ";
            }
            query += $") VALUES\n";
            foreach (T value in values)
            {
                query += $"(";
                foreach(var attr in value.GetType().GetProperties())
                {
                    if (attr.Name == "id") continue;

                    if (attr.PropertyType == typeof(string))
                        query += $"'{attr.GetValue(value)}', ";
                    else 
                        query += $"{attr.GetValue(value)}, ";
                }
                query += $"),\n";
            }
            query += ";";
            query = query.Replace(", )", ")").Replace(",\n;", "\n;");
            return query;

            
        }


    }
}
