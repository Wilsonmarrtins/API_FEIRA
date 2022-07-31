using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_Data.DapperConfig
{
    public class Conexao
    {
        readonly MySqlConnection con = new();
        public Conexao()
        {
            con.ConnectionString = Setting.ConnectionString;
        }
        public MySqlConnection Conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                if (String.IsNullOrEmpty(con.ConnectionString))
                {
                    con.ConnectionString = Setting.ConnectionString;
                }
                con.Open();
            }
            return con;
        }
        public void Desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        protected async Task<IEnumerable<T>> ExecutaSelectLista<T>(string query)
        {
            using var connection = Conectar();
            return await connection.QueryAsync<T>(query);
        }
        protected async Task<T> ExecutaSelect<T>(string sqlQuery)
        {
            using var connection = Conectar();
            return await connection.QueryFirstOrDefaultAsync<T>(sqlQuery);
        }
        protected async Task<bool> ExecutaComando(string sqlQuery)
        {
            using var connection = Conectar();
            return await connection.ExecuteAsync(sqlQuery) > 0;
        }
        protected async Task<T> EXECUTAPROC<T>(string sqlQuery)
        {
            using var connection = Conectar();
            return await connection.QueryFirstOrDefaultAsync<T>(sqlQuery);
        }

        protected async Task<bool> UpdateContrib<T>(T Objeto) where T : class
        {
            using var connection = Conectar();
            return await connection.UpdateAsync(Objeto);
        }
    }
}
