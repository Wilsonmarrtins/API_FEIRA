using ServicoImport.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoImport.Dados
{
    public class Query : Conexao
    {
        public List<Arquivo> Salvar(List<Arquivo> lista)
        {
            try
            {
                foreach (var item in lista)
                {
                    try
                    {
						var sqlQuery = $@"insert into feira(LON,LAT,SETCENS,AREAP,CODDIST,DISTRITO,CODSUBPREF,SUBPREFE,REGIAO5,REGIAO8,NOME_FEIRA,REGISTRO,LOGRADOURO,NUMERO,BAIRRO,REFERENCIA) values 
                       ('{item.LON}','{item.LAT}','{item.SETCENS}','{item.AREAP}','{item.CODDIST}','{item.DISTRITO}','{item.CODSUBPREF}','{item.SUBPREFE}','{item.REGIAO5}','{item.REGIAO8}','{item.NOME_FEIRA}','{item.REGISTRO}','{item.LOGRADOURO}','{item.NUMERO}','{item.BAIRRO}','{item.REFERENCIA}')";
						var retorno = ExecutaComando(sqlQuery);
                        item.Sucesso = retorno;


					}
                    catch (Exception Ex)
                    {
                        var erro = Ex.Message;
                        continue;
                    }
                }
            }
            catch (Exception Ex)
            {
				var erro = Ex.Message;
            }

            return lista;
        }
    }
}
