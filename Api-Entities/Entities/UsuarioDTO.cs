using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Entities.Entities
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }
    }
}
