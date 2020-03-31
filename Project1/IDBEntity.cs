using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public interface IDBEntity<T> where T : struct
    {
        T Id { set; get; }
    }
}
