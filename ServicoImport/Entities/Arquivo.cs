using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoImport.Entities
{
    public class Arquivo
    {
        public string ID { get; set; }
        public string LON { get; set; }
        public string LAT { get; set; }
        public string SETCENS { get; set; }
        public string AREAP { get; set; }
        public string CODDIST { get; set; }
        public string DISTRITO { get; set; }
        public string CODSUBPREF { get; set; }
        public string SUBPREFE { get; set; }
        public string REGIAO5 { get; set; }
        public string REGIAO8 { get; set; }
        public string NOME_FEIRA { get; set; }
        public string REGISTRO { get; set; }
        public string LOGRADOURO { get; set; }
        public string NUMERO { get; set; }
        public string BAIRRO { get; set; }
        public string REFERENCIA { get; set; }
        public bool Sucesso { get; set; }
    }
}
