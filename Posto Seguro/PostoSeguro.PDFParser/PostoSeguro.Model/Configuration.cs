using System;

namespace PostoSeguro.Model
{
    public class Configuration : EntityBase
    {
        public string Name { get; set; }
        public DateTime UltimaAtualizacaoDadosBombaMedidora { get; set; }
        public DateTime UltimaAtualizacaoQualidade { get; set; }
    }
}