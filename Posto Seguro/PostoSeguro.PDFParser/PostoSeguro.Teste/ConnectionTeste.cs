using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostoSeguro.Data;

namespace PostoSeguro.Teste
{
    [TestClass]
    public class ConnectionTeste
    {
        [TestMethod]
        public void ConectarNoMongoDB()
        {
            Assert.IsTrue(MongoConnection.OpenConnection());
        }
    }
}
