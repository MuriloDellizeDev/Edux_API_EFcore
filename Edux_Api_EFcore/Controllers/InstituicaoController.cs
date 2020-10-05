using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux_Api_EFcore.Domains;
using Edux_Api_EFcore.Interfaces;
using Edux_Api_EFcore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edux_Api_EFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController()
        {
            _instituicaoRepository = new InstituicaoRepository();
        }


        /// <summary>
        /// Mostra todas as Instituições cadastradas
        /// </summary>
        /// <returns>Lista com todos as Instituições</returns>

        // GET: api/<InstituicaoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var instituicoes = _instituicaoRepository.Mostrar();
                if (instituicoes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(instituicoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Mostra uma única Instituições especificado pelo seu ID
        /// </summary>
        /// <param name="id">ID da Instituição</param>
        /// <returns>Uma Instituição</returns>
        
        // GET api/<InstituicaoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var instituicao = _instituicaoRepository.BuscarPorId(id);
                if (instituicao == null)
                {
                    return NotFound();
                }
                return Ok(instituicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Mostra uma única Categoria especificado pelo seu NOME
        /// </summary>
        /// <param name="nome">Objeto nome</param>
        /// <returns>Uma Instituição</returns>
 
        // GET api/<ObjetivoController>/
        [HttpGet("{nome}")]
        public IActionResult Get(string nome)
        {
            try
            {

                var instituicoes = _instituicaoRepository.BuscarPorNome(nome);
                var qtdInstituicoes = instituicoes.Count;

                if (qtdInstituicoes == 0)
                {
                    return NoContent();
                }

                return Ok(new
                {
                    totalCount = qtdInstituicoes,
                    data = instituicoes
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }



        /// <summary>
        /// Cadastra uma nova Instituição
        /// </summary>
        /// <param name="instituicao">Objeto Instituição</param>
        /// <returns>Info alterada da Instituição</returns>
   
        // POST api/<InstituicaoController>
        [HttpPost]
        public IActionResult Post(Instituicao instituicao)
        {
            try
            {
                _instituicaoRepository.Adicionar(instituicao);

                return Ok(instituicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Altera uma determinada Instituição
        /// </summary>
        /// <param name="instituicao">Objeto Instituição com as alterações</param>
        /// <returns>Informações alterada da Inatituição</returns>
     
        // PUT api/<InstituicaoController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Instituicao instituicao)
        {
            try
            {
                _instituicaoRepository.Editar(instituicao);

                return Ok(instituicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Excluí uma determinada Instituição
        /// </summary>
        /// <param name="id">ID da Instituição</param>
        /// <returns>ID Excluído</returns>
      
        // DELETE api/<InstituicaoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _instituicaoRepository.Remover(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }}

    }
}
