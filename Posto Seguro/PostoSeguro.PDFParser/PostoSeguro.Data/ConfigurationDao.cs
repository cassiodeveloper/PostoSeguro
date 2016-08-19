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

        public DateTime ObterUltimaAtualizacaoBombaMedidora()
        {
            return configRepo.SearchFor(c => c.Name == "UltimaAtualizacaoBombaMedidora")
                .SingleOrDefault()
                .UltimaAtualizacaoDadosBombaMedidora;
        }

        public bool AtualizarUltimaAtualizacaoBombaMedidora(Configuration configuration)
        {
            return configRepo.Update(configuration);
        }

        public DateTime ObterUltimaAtualizacaoQualidade()
        {
            return configRepo.SearchFor(c => c.Name == "UltimaAtualizacaoQualidade")
                .SingleOrDefault()
                .UltimaAtualizacaoQualidade;
        }
    }
}