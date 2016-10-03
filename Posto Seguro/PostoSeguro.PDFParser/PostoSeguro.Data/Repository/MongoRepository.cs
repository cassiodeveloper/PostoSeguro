using MongoDB.Bson;
using MongoDB.Driver;
using PostoSeguro.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace PostoSeguro.Data.Repository
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        #region Private Members

        private IMongoDatabase database;
        private IMongoCollection<TEntity> collection;

        #endregion

        #region Constructors

        public MongoRepository()
        {
            GetDatabase();
            GetCollection();
        }

        #endregion

        #region Public Methods

        public bool Insert(TEntity entity)
        {
            entity.Id = BsonObjectId.GenerateNewId();

            return collection.InsertOneAsync(entity).IsCompleted;
        }

        public bool Update(TEntity entity)
        {
            var result = collection.ReplaceOneAsync(CreateFilterByDefaultId(entity), entity);

            return result.Result.MatchedCount > 0;
        }

        public bool Delete(TEntity entity)
        {
            var result = collection.DeleteManyAsync(CreateFilterByEntityId(entity));

            return result.Result.DeletedCount > 0;
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return collection.FindAsync<TEntity>(new BsonDocument()).Result.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return collection.FindAsync<TEntity>(CreateFilterById(id)).Result.ToList().SingleOrDefault();
        }

        public TEntity GetById(TEntity entity)
        {
            return collection.FindAsync<TEntity>(CreateFilterByDefaultId(entity)).Result.ToList().SingleOrDefault();
        }

        #endregion

        #region Private Helper Methods

        private void GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            database = client.GetDatabase(GetDatabaseName());
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["MongoDbConnectionString"].Replace("{DB_NAME}", GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbDatabaseName");
        }

        private void GetCollection()
        {
            collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        private FilterDefinition<TEntity> CreateFilterById(Guid id)
        {
            return Builders<TEntity>.Filter.Eq("id", id);
        }

        private FilterDefinition<TEntity> CreateFilterByDefaultId(TEntity entity)
        {
            return Builders<TEntity>.Filter.Eq("_id", entity.Id);
        }

        private FilterDefinition<TEntity> CreateFilterByDefaultId(string id)
        {
            return Builders<TEntity>.Filter.Eq("_id", id);
        }

        private FilterDefinition<TEntity> CreateFilterByEntityId(TEntity entity)
        {
            return Builders<TEntity>.Filter.Eq("id", entity.Id);
        }

        #endregion
    }
}