using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project1.MongoDB.Interfaces;

namespace Project1.MongoDB.Repository.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : IMongoEntity
    {

        #region AsyncMethods
        Task<Guid> InsertAsync ( TEntity entity );
        Task<IEnumerable<TEntity>> GetAllDataAsync ();
        Task<TEntity> GetDataByIdAsync ( Guid id );
        Task<TEntity> GetDataByExpressionAsync ( Expression<Func<TEntity, bool>> expression );
        Task UpdateAsync ( TEntity entity );
        Task DeleteAsync ( Guid id );
        #endregion
    }
}
