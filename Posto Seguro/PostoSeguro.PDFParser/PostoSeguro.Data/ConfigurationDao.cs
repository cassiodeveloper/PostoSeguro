using PostoSeguro.Data.Repository;
using PostoSeguro.Model;
using System;
using System.Linq;

namespace PostoSeguro.Data
{
    public class ConfigurationDao
    {
        Configuration configuration = new Configuration();
        MongoRepository<Configuration> configRepo = new MongoRepository<Configuration>();

        public Configuration ObterEntidadeUltimaAtualizacao()
        {
            return configRepo.SearchFor(c => c.Name == "UltimaAtualizacao")
                .SingleOrDefault();
        }

        public bool AtualizarUltimaDataAtualizacao(Configuration configuration)
        {
            return configRepo.Update(configuration);
        }
    }
}