using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    if (attr.Name.EndsWith("_id"))
                    {

                        //var selectBuilder = new SQLBuilder<>;
                        query += $"(SELECT id from {attr.Name.Replace("_id", "")} WHERE name LIKE '{attr.GetValue(value)}'), ";
                    }
                    else if (attr.PropertyType == typeof(string))
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

        public string BuildSelect(IDictionary<string, string> where = null, string orderby = "")
        {
            return BuildSelect("*", where, orderby);
        }

        public string BuildSelect(string field, IDictionary<string, string> where = null, string orderby = "")
        {
            string query = $"SELECT {field} FROM {typeof(T).Name} ";
            if (where != null)
            {
                query += "WHERE 1=1 ";
                foreach (var attr in where)
                {
                    string attr_type = typeof(T).GetProperties().Where(t => t.Name == attr.Key).GetType().Name;
                    string attr_value = attr_type == "string" ? $" LIKE '{attr.Value}'" : $" = {attr.Value}";
                    query += $"AND {typeof(T).Name}.{attr.Key} {attr_value}";
                }
            }
            query += ";";
            return query;
        }

        private readonly Dictionary<string, string> EMPTY = new Dictionary<string, string>();
    }
}
