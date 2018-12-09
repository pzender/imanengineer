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
            foreach (var attr in typeof(T).GetProperties())
            {
                if (attr.Name == "id") continue;
                query += $"{attr.Name}, ";
            }
            query += $") VALUES\n";
            foreach (T value in values)
            {
                query += $"(";
                foreach (var attr in value.GetType().GetProperties())
                {
                    if (attr.Name == "id") continue;
                    else if (attr.PropertyType == typeof(string) || attr.PropertyType == typeof(DateTime))
                        query += $"'{attr.GetValue(value).ToString().Replace("'", "''")}', ";
                    else
                        query += $"{attr.GetValue(value)}, ";
                }
                query += $"),\n";
            }
            query += ";";
            query = query.Replace(", )", ")").Replace(",\n;", "\n;");
            return query;


        }

        public string BuildSelect(string field = "*", string from = "$TABLE", IDictionary<string, string> where = null, string orderby = "")
        {
            if (from == "$TABLE") from = typeof(T).Name;
            string query = $"SELECT {field} FROM {from} ";
            if (where != null) query += BuildWhere(where);
            if (orderby != "") query += $"ORDER BY {orderby} ";
            query += ";";
            return query;
        }

        public string BuildUpdate(IDictionary<string, string> set, IDictionary<string, string> where = null)
        {
            string table = typeof(T).Name;
            string query = $"UPDATE {table} SET ";
            foreach (var attr in set)
            {
                Type attr_type = typeof(T).GetProperty(attr.Key).PropertyType;
                string attr_value = "";
                if (attr_type == typeof(string) || attr_type == typeof(DateTime))
                    attr_value = $" = '{attr.Value.Replace("'", "''")}'";
                else attr_value = $" = {attr.Value}";
                query += $"{typeof(T).Name}.{attr.Key}{attr_value}";
            }
            query += "\n";
            if (where != null) query += BuildWhere(where);
            query = query.Replace(", )", ")").Replace(",\n;", "\n;");
            query += ";";
            return query;

            //		actual	"UPDATE Series SET title = Kung Fu Panda, WHERE 1=1 AND Series.title LIKE 'Kung Fu Panda - legenda o niezwykłości' "	string

        }

        private string BuildWhere(IDictionary<string, string> conditions)
        {
            string clause = "WHERE 1=1 ";
            foreach (var attr in conditions)
            {
                clause += $"AND {BuildFromKVPair(attr)} ";
            }
            return clause;
        }

        private string BuildFromKVPair(KeyValuePair<string, string> pair)
        {
            {
                Type attr_type = typeof(T).GetProperty(pair.Key).PropertyType;
                string attr_value = "";
                if (attr_type == typeof(string))
                    attr_value = $" LIKE '{pair.Value.Replace("'", "''")}'";
                else if (attr_type == typeof(DateTime))
                    attr_value = $" = '{pair.Value}'";
                else attr_value = $" = {pair.Value}";

                return $"{typeof(T).Name}.{pair.Key}{attr_value}";
            }

            //private readonly Dictionary<string, string> EMPTY = new Dictionary<string, string>();
        }
    }
}
