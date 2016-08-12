using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostoSeguro.Data;
using PostoSeguro.Data.Repository;
using PostoSeguro.Model;
using System;
using System.Linq;

namespace PostoSeguro.Teste
{
    [TestClass]
    public class ConnectionTeste
    {
        [TestMethod]
        public void BuscaUltimaAtualizacaoDadosBombaMedidora()
        {
            MongoRepository<Configuration> configRepo = new MongoRepository<Configuration>();

            DateTime data = configRepo.SearchFor(c => c.Name == "UltimaAtualizacaoBombaMedidora").SingleOrDefault().UltimaAtualizacaoDadosBombaMedidora;

            Assert.IsNotNull(data);
        }
    }
}