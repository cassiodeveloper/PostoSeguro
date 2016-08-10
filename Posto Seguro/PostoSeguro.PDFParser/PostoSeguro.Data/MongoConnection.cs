using MongoDB.Driver;
using System;

namespace PostoSeguro.Data
{
    public class MongoConnection
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected static string connectionString = "mongodb://127.0.0.1/PostoSeguro";

        public static bool OpenConnection()
        {
            try
            {
                _client = new MongoClient(connectionString);
                _database = _client.GetDatabase("PostoSeguro");
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}