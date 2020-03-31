using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Project1.CustomExceptions;
using Project1.MongoDB.Interfaces;
using Project1.MongoDB.Repository.Interfaces;
using Service.DataAccess.MongoDB;

namespace Project1.MongoDB.Repository.Implementation
{
    public abstract class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : IMongoEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;
        public MongoRepository ( AppDbContext db, string collectionName )
        {
            _collection = db.GetCollectionByName<TEntity> ( collectionName );
        }


        #region AsyncMethods
        public virtual async Task DeleteAsync ( Guid id )
        {
            try
            {
                await _collection.FindOneAndDeleteAsync ( e => e.Id == id );
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Delete entity from DB failed", exception );
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllDataAsync ()
        {
            try
            {
                var query = await _collection.FindAsync ( _ => true );
                return query.Current;
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Getting data from DB failed", exception );
            }

        }
        public virtual async Task<TEntity> GetDataByIdAsync ( Guid id )
        {
            try
            {
                return await _collection.FindAsync ( s => s.Id == id ).Result.FirstOrDefaultAsync ();
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Getting entity from DB failed", exception );
            }
        }
        public virtual async Task<TEntity> GetDataByExpressionAsync ( Expression<Func<TEntity, bool>> expression )
        {
            try
            {
                return await _collection.FindAsync ( expression ).Result.FirstOrDefaultAsync ();
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Getting data from DB failed", exception );
            }
        }
        public virtual async Task<Guid> InsertAsync ( TEntity entity )
        {
            var csuuid = ConvertFromGUIDtoCSUUID ( Guid.NewGuid () );
            entity.Id = csuuid;
            try
            {
                await _collection.InsertOneAsync ( entity );
                return entity.Id;
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Insert entity into DB failed", exception );
            }
        }
        public virtual async Task UpdateAsync ( TEntity entity )
        {
            try
            {
                await _collection.ReplaceOneAsync ( e => e.Id == entity.Id, entity , new ReplaceOptions () { IsUpsert = true} );
            }
            catch ( MongoException exception )
            {
                throw new DatabaseException ( "Updating entity in DB failed", exception );
            }
        }
        #endregion



        #region Helpers
        private Guid ConvertFromGUIDtoCSUUID ( Guid id )
        {
            var bytes = GuidConverter.ToBytes ( id, GuidRepresentation.PythonLegacy );
            return new Guid ( bytes );
        }
        #endregion
    }
}
