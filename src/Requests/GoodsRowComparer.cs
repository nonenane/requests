using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Requests
{
    class GoodsRowComparer : IEqualityComparer<DataRow>
    {
        public bool Equals(DataRow x, DataRow y)
        {
            return x["id_tovar"].ToString() == y["id_tovar"].ToString();
        }

        public int GetHashCode(DataRow obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
