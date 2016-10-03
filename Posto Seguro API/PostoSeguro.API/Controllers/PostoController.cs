using PostoSeguro.Data;
using PostoSeguro.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PostoSeguro.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostoController : ApiController
    {
        public PostoController()
        {

        }

        PostoDao postoDao = new PostoDao();

        public IEnumerable<Posto> Get()
        {
            return postoDao.ObterPostos();
        }

        public Posto Get(string Id)
        {
            return postoDao.ObterPosto(Id);
        }
    }
}