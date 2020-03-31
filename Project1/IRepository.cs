using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public interface IRepository<TEntity, T>
        where T: struct
        where TEntity : IDBEntity<T>
    {
        bool Insert (TEntity newEntity);
        List<TEntity> FindAll ();
        TEntity FindById ();
    }
}
