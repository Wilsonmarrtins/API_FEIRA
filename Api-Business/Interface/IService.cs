using api_Data.Interface;
using Api_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Business.Interface
{
    public interface IService
    {
        Task<bool> Usuario(UsuarioDTO Usuario);
        Task<bool>Cadastrar(FeiraDTO Entrada);
        Task<bool>Delete (int Codigo);
        Task<List<FeiraDTO>>Consulta(string regiao5);
        Task<bool>Alterar(FeiraDTO Entrada);
        Task<FeiraData> ConverteObjeto(FeiraDTO O);
    }
}
