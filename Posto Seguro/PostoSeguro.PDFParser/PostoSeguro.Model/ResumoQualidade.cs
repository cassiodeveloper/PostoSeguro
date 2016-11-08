using System.Collections.Generic;

namespace PostoSeguro.Model
{
    public class ResumoQualidade : EntityBase
    {
        public string Estado { get; set; }
        public int AgentesFiscalizados { get; set; }
        public int AutosDeInfracao { get; set; }
        public int AutosDeInterdicao { get; set; }
        public List<Obs> Observacoes { get; set; }
    }
}