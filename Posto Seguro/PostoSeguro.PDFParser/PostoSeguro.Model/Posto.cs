using System;
using System.Collections.Generic;

namespace PostoSeguro.Model
{
    public class Posto : EntityBase
    {
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Endereco { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Tipo { get; set; }
        public string Bandeira { get; set; }
        public IList<Penalidade> Penalidades { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Liberado { get; set; }
    }
}