using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostoSeguro.API.Controllers;
using System.Collections.Generic;

namespace PostoSeguro.API.Tests.Controllers
{
    [TestClass]
    public class PostoControllerTest
    {
        PostoController controller;

        [TestInitialize]
        private void ConfigurarTeste()
        {
            controller = new PostoController();
        }

        [TestMethod]
        public void CarregarTodosOsPostos()
        {
            IEnumerable<Model.Posto> postos = controller.Get();

            Assert.IsNotNull(postos);
        }
    }
}
