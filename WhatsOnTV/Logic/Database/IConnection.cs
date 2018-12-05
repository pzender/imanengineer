using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database
{
    public interface IConnection<T1, T2>
    {
        int t1_id { get; set; }
        int t2_id { get; set; }

        string table { get; }
    }
}
