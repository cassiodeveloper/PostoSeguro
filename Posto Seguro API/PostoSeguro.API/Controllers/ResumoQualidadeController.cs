using PostoSeguro.Data;
using PostoSeguro.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PostoSeguro.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ResumoQualidadeController : ApiController
    {
        ResumoQualidadeDao resumoQualidadeDao = new ResumoQualidadeDao();

        public ResumoQualidadeController()
        {

        }

        public ResumoQualidade Get(string Id)
        {
            return resumoQualidadeDao.ObterResumoPorEstado(Id);
        }

        //public IEnumerable<ResumoQualidade> Get()
        //{
        //    return resumoQualidadeDao.ObterResumosQualidade();
        //}
    }
}