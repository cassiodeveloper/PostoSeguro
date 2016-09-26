using PostoSeguro.Data.Repository;
using PostoSeguro.Model;
using System.Collections.Generic;

namespace PostoSeguro.Data
{
    public class PostoDao
    {
        MongoRepository<Posto> postoRepo = new MongoRepository<Posto>();

        public IList<Posto> ObterPostos()
        {
            return postoRepo.GetAll();
        }

        public Posto ObterPosto(int Id)
        {
            return new Posto();
            //return postoRepo.SearchFor(c => c.Id == Id).SingleOrDefault();
        }
    }
}