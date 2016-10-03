using PostoSeguro.Data.Repository;
using PostoSeguro.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostoSeguro.Data
{
    public class PostoDao
    {
        MongoRepository<Posto> postoRepo = new MongoRepository<Posto>();

        public IList<Posto> ObterPostos()
        {
            return postoRepo.GetAll();
        }

        public Posto ObterPosto(string Id)
        {
            Posto posto = new Posto() { Id = new MongoDB.Bson.BsonObjectId(new MongoDB.Bson.ObjectId(Id)) };
            return postoRepo.GetById(posto);
        }
    }
}