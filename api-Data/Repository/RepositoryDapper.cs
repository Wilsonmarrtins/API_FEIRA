
using api_Data.DapperConfig;
using api_Data.Interface;
using Api_Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Data.Repository
{
    public class RepositoryDapper : Conexao, IRepository
    {
        public async Task<UsuarioDTO> Usuario(UsuarioDTO Usuario)
        {
            Log.Information($"Entrando no metodo Usuario na classe RepositoryDapper");
            var retorno = new UsuarioDTO();
            try
            {
                var sqlQuery = $@"select * from usuario where usuario = '{Usuario.Usuario}' and senha = '{Usuario.Senha}';";
                Log.Information($"Persistindo os dados no BD");
                retorno = await ExecutaSelect<UsuarioDTO>(sqlQuery);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Exception no medodo Usuario na classe RepositoryDapper.cs, erro: {erro}");
            }

            return retorno;

        }
        public async Task<bool> Salvar(FeiraDTO item)
        {
            Log.Information($"Entrando no metodo Salvar na classe RepositoryDapper");
            var retorno = false;
            try
            {
                Log.Information($"Persistindo os dados no BD");
                var sqlQuery = $@"insert into feira(LON,LAT,SETCENS,AREAP,CODDIST,DISTRITO,CODSUBPREF,SUBPREFE,REGIAO5,REGIAO8,NOME_FEIRA,REGISTRO,LOGRADOURO,NUMERO,BAIRRO,REFERENCIA) values 
             ('{item.LON}','{item.LAT}','{item.SETCENS}','{item.AREAP}','{item.CODDIST}','{item.DISTRITO}','{item.CODSUBPREF}','{item.SUBPREFE}','{item.REGIAO5}','{item.REGIAO8}','{item.NOME_FEIRA}','{item.REGISTRO}','{item.LOGRADOURO}','{item.NUMERO}','{item.BAIRRO}','{item.REFERENCIA}')";
              retorno = await ExecutaComando(sqlQuery);

            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Exception no medodo Salvar na classe RepositoryDapper.cs, erro: {erro}");
            }
            return retorno;
        }
        public async Task<bool> Excluir(int Codigo)
        {
            Log.Information($"Entrando no metodo Excluir na classe RepositoryDapper");
            var retorno = false;
            try
            {
                Log.Information($"Persistindo os dados no BD");
                var sqlQuery = $@"delete from feira where id = {Codigo}";
                retorno = await ExecutaComando(sqlQuery);

            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Exception no medodo Excluir na classe RepositoryDapper.cs, erro: {erro}");
            }

            return retorno;
        }
        public async Task<List<FeiraDTO>> Consulta(string regiao5)
        {
            Log.Information($"Entrando no metodo Consulta na classe RepositoryDapper");
            var retorno = new List<FeiraDTO>();
            try
            {
                Log.Information($"Persistindo os dados no BD");
                var sqlQuery = $@"select * from feira where regiao5 = '{regiao5}'";
                var a = await ExecutaSelectLista<FeiraDTO>(sqlQuery);
                retorno = a.ToList();
            }
            catch (Exception EX)
            {
                var erro = EX.Message;
                Log.Fatal($"Exception no medodo Consulta na classe RepositoryDapper.cs, erro: {erro}");

            }
            return retorno;
        }
        public async Task<bool> Alterar(FeiraData item)
        {
            Log.Information($"Entrando no metodo Alterar na classe RepositoryDapper");
            var retorno = false;
            try
            {
                Log.Information($"Persistindo os dados no BD");
                retorno = await UpdateContrib(item);
            }
            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Exception no medodo Alterar na classe RepositoryDapper.cs, erro: {erro}");
            }

            return retorno;
        }
    }
}
