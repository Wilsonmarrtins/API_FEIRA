
using Api_Business.Interface;
using Api_Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_feira_livre_unico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeiraController : ControllerBase
    {
        private readonly IService _Service;
        public FeiraController(IService Service)
        {
            _Service = Service;
        }
        [Authorize("Bearer")]
        [HttpPost, Route("Cadastro")]
        public async Task<ActionResult<string>> Cadastro(FeiraDTO Entrada)
        {
            Log.Information("Entrando no metodo de Cadastro na controller Feira");
            string mensagem = string.Empty;
            try
            {
                var retorno = await _Service.Cadastrar(Entrada);
                if (retorno == false)
                {
                    
                    mensagem = "Cadastro não realizado!";
                    Log.Information($"{mensagem}");
                    return BadRequest(mensagem);
                }
                else
                {
                    mensagem = "Cadastro realizado com sucesso!";
                    Log.Information($"{mensagem}");
                }
            }

            catch (Exception ex)
            {
                var erro = ex.Message;
                Log.Fatal($"Erro ao cadastrar a feira: {erro}");
                return BadRequest("Algo deu errado entre em contato com o suporte!");
            }

            return Ok(mensagem);
        }
        [Authorize("Bearer")]
        [HttpDelete, Route("Exclusao")]
        public async Task<ActionResult<string>> Exclusao(int Codigo)
        {
            Log.Information("Entrando no metodo de Exclusao na controller Feira");
            string mensagem = string.Empty;

            try
            {
                var retorno = await _Service.Delete(Codigo);

                if (retorno == false)
                {
                    Log.Information("O registro não pode ser excluido!");
                    return BadRequest("O registro não pode ser excluido!");
                }
                else
                {
                    mensagem = "Registro excluido com sucesso!";
                    Log.Information($"{mensagem}");
                }
            }

            catch (Exception ex)
            {
                var erro = ex.Message;
                Log.Information($"Erro metodo de exclusão erro: {erro}");
                return BadRequest("Algo deu errado entre em contato com o suporte!");
            }

            return Ok(mensagem);
        }
        [Authorize("Bearer")]
        [HttpGet, Route("Consulta")]
        public async Task<ActionResult<FeiraDTO>> Consulta(string Regiao5)
        {
            Log.Information("Entrando no metodo de consulta na controller Feira");
            var resultado = new List<FeiraDTO>();
            try
            {
                resultado = await _Service.Consulta(Regiao5);
                if (resultado.Count == 0)
                {
                    Log.Information("Não foi encontrado nenhum registro");
                    return BadRequest("Nenhum registrado localizado!");
                }
            }

            catch (Exception Ex)
            {
                var erro = Ex.Message;
                Log.Fatal($"Erro ao tentar consultar erro: {erro}");
                return BadRequest("Algo deu errado entre em contato com o suporte!");
            }

            return Ok(resultado);
        }
        [Authorize("Bearer")]
        [HttpPut, Route("Alteracao")]
        public async Task<ActionResult<string>> Alteracao(FeiraDTO Entrada)
        {
            Log.Information("Entrando no metodo de Alteracao na controller Feira");
            string mensagem = string.Empty;
            try
            {
                var retorno = await _Service.Alterar(Entrada);
                if (retorno == false)
                {
                    mensagem = "Não foi possivel realizar a alteração do registro!";
                    Log.Information($"{mensagem}");
                    return BadRequest(mensagem);
                }
                else
                {
                    mensagem = "Cadastro alterado com sucesso!";
                    Log.Information($"{mensagem}");
                }
            }

            catch (Exception ex)
            {
                var erro = ex.Message;
                Log.Error($"erro no metodo Alteracao no controller Feira, erro: {erro}");
                return BadRequest("Algo deu errado entre em contato com o suporte!");
            }

            return Ok(mensagem);
        }
    }
}
