using Api_Business.Interface;
using api_Data.Interface;
using Api_Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Business.Service
{
    public class Service: IService
    {
        private readonly IRepository _Repository;
        public Service(IRepository Repository)
        {
            _Repository = Repository;
        }

        public async Task<bool> Usuario(UsuarioDTO Usuario)
        {
            Log.Information("Entrando no metodo de Usuario na classe: Service");
            var retorno = false;
            try
            {
                Log.Information("chamando o metodo de Usuario em _Repository");
                var consulta = await _Repository.Usuario(Usuario);
                if (!String.IsNullOrEmpty(consulta.Usuario))
                {
                    Log.Information("Usuario localizado!");
                    retorno = true;
                }
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Erro metodo de consulta de usuario na classe Service: erro {erro}");
            }

            return retorno;
        }
        public async Task<bool> Cadastrar(FeiraDTO Entrada)
        {
            Log.Information("Entrando no metodo de Cadastrar na classe: Service");
            var resultado = false;
            try
            {
                Log.Information("chamando o metodo de salvar em _Repository");
                resultado = await _Repository.Salvar(Entrada);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Erro metodo de cadastrar na classe Service: {erro}");
            }

            return resultado;
        }
        public async Task<bool> Delete(int Codigo)
        {
            Log.Information("Entrando no metodo de Delete na classe: Service");
            var resultado = false;
            try
            {
                Log.Information("Entrando no metodo de Deleteem _Repository");
                resultado = await _Repository.Excluir(Codigo);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Erro metodo de Delete na classe Service.cs, erro: {erro}");
            }
            return resultado;
        }
        public async Task<List<FeiraDTO>> Consulta(string regiao5)
        {
            Log.Information("Entrando no metodo de Consulta na classe: Service.cs");
            var resultado = new List<FeiraDTO>();
            try
            {
                Log.Information("Entrando no metodo de Consulta em _Repository");
                resultado = await _Repository.Consulta(regiao5);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Erro no metodo de Consulta na classe Service.cs, erro: {erro}");
            }
            return resultado;
        }
        public async Task<bool> Alterar(FeiraDTO Entrada)
        {
            Log.Information("Entrando no metodo de Alterar na classe: Service.cs");
            var resultado = false;
            try
            {
                Log.Information($"Chamando metodo de Converter objeto");
                var Objt = await ConverteObjeto(Entrada);
                Log.Information($"Chamando metodo de Alterar em _Repository");
                resultado = await _Repository.Alterar(Objt);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Exception no metodo de altarar na classe Service.cs, erro: {erro}");
            }

            return resultado;
        }
        public async Task<FeiraData> ConverteObjeto(FeiraDTO O)
        {
            var retorno = new FeiraData()
            {
                ID          = O.ID,
                LON         = O.LON,
                LAT         = O.LAT,
                SETCENS     = O.SETCENS,
                AREAP       = O.AREAP,
                CODDIST     = O.CODDIST,
                DISTRITO    = O.DISTRITO,
                CODSUBPREF  = O.CODSUBPREF,
                SUBPREFE    = O.SUBPREFE,
                REGIAO5     = O.REGIAO5,
                REGIAO8     = O.REGIAO8,
                NOME_FEIRA  = O.NOME_FEIRA,
                REGISTRO    = O.REGISTRO,
                LOGRADOURO  = O.REGISTRO,
                NUMERO      = O.NUMERO,
                BAIRRO      = O.BAIRRO,
                REFERENCIA  = O.REFERENCIA
            };

            return retorno;
        }
    }
}
