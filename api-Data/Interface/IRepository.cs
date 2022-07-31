using Api_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Data.Interface
{
    public interface IRepository
    {
        Task<UsuarioDTO> Usuario(UsuarioDTO Usuario);
        Task<bool> Salvar(FeiraDTO item);
        Task<bool> Excluir(int Codigo);
        Task<List<FeiraDTO>> Consulta(string regiao5);
        Task<bool> Alterar(FeiraData item);
    }
}
