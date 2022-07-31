using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoImport.Dados
{
    public class Conexao
    {
        MySqlConnection con = new MySqlConnection();
        public Conexao()
        {
            con.ConnectionString = "Server=robb0260.publiccloud.com.br; Port=3306; Database=maochuen_wilson; Uid=maoch_wilson; Pwd=Guilherme123!@#;";
        }

        public MySqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                if (String.IsNullOrEmpty(con.ConnectionString))
                {
                    con.ConnectionString = "Server=robb0260.publiccloud.com.br; Port=3306; Database=maochuen_wilson; Uid=maoch_wilson; Pwd=Guilherme123!@#;";
                }
                con.Open();
            }

            return con;
        }

        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        protected List<T> ExecutaSelectLista<T>(string query)
        {
            using (var connection = conectar())
            {
                return connection.Query<T>(query).AsList();
            }
        }
        protected T ExecutaSelect<T>(string sqlQuery)
        {
            using (var connection = conectar())
            {
                return connection.QueryFirstOrDefault<T>(sqlQuery);
            }
        }

        protected bool ExecutaComando(string sqlQuery)
        {
            using (var connection = conectar())
            {
                return connection.Execute(sqlQuery) > 0;
            }
        }

        protected T EXECUTAPROC<T>(string sqlQuery)
        {
            using (var connection = conectar())
            {
                return connection.QueryFirstOrDefault<T>(sqlQuery);
            }
        }
    }
}
