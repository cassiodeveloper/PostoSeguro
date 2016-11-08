using PostoSeguro.Data.Repository;
using PostoSeguro.Model;
using System.Collections.Generic;
using System.Linq;

namespace PostoSeguro.Data
{
    public class ResumoQualidadeDao
    {
        MongoRepository<ResumoQualidade> resumoRepo = new MongoRepository<ResumoQualidade>();

        public ResumoQualidade ObterResumoPorEstado(string estado)
        {
            return resumoRepo.SearchFor(c => c.Estado == estado).SingleOrDefault();
        }

        public IList<ResumoQualidade> ObterResumosQualidade()
        {
            return resumoRepo.GetAll();
        }
    }
}