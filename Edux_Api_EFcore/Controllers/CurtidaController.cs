﻿using System;
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
    public class CurtidaController : ControllerBase
    {

        private readonly ICurtidaRepository _curtidaRepository;

        public CurtidaController()
        {

            _curtidaRepository = new CurtidaRepository();

        }


        /// <summary>
        /// Mostra todas as Curtidas cadastradas
        /// </summary>
        /// <returns>Lista com todos as Curtidas</returns>
        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<CurtidaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var curtidas = _curtidaRepository.Mostrar();
                var qtdCurtidas = curtidas.Count;

                if (qtdCurtidas == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdCurtidas,
                    data = curtidas
                });


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }
        }


        /// <summary>
        /// Mostra uma única Curtida especificado pelo seu ID
        /// </summary>
        /// <param name="id">ID da Curtida</param>
        /// <returns>Uma Curtida</returns>
        [Authorize(Roles = "Professor")]
        // GET api/<CurtidaController>/5
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var curtidas = _curtidaRepository.BuscarPorId(id);

                if (curtidas == null)
                    return NotFound();

                return Ok(curtidas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }
        }


        /// <summary>
        /// Cadastra uma nova Curtida
        /// </summary>
        /// <param name="curtida">Objeto Curtida</param>
        /// <returns>Curtida Cadastrado</returns>
        [Authorize(Roles = "Aluno,Professor")]
        // POST api/<CurtidaController>
        [HttpPost]
        public IActionResult Post([FromBody] Curtida curtida)
        {

            try
            {

                curtida = _curtidaRepository.Adicionar(curtida);
                return Ok(curtida);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");


            }

        }


        /// <summary>
        /// Altera um determinado Curtida
        /// </summary>
        /// <param name="curtida">Objeto Curtida com as alterações</param>
        /// <returns>Informações alteradas do Curtida </returns>
        [Authorize(Roles = "Aluno,Professor")]
        // PUT api/<CurtidaController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Curtida curtida)
        {

            try
            {
                _curtidaRepository.Editar(curtida);

                return Ok(curtida);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }


        /// <summary>
        /// Excluí um determinada Curtida
        /// </summary>
        /// <param name="id">ID da Curtida</param>
        /// <returns>ID excluído</returns>
        [Authorize(Roles = "Aluno,Professor")]
        // DELETE api/<CurtidaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            try
            {
                _curtidaRepository.Remover(id);


                return Ok(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }
    }
}
