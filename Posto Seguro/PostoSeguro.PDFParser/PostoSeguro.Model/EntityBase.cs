using MongoDB.Bson;

namespace PostoSeguro.Model
{
    public abstract class EntityBase
    {
        public BsonObjectId Id { get; set; }
    }
}