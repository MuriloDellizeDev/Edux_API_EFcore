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
    public class PerfilController : ControllerBase
    {
        private IPerfilRepository _perfilRepository;

        public PerfilController()
        {
            _perfilRepository = new PerfilRepository();
        }

        /// <summary>
        /// Mostra todos os Perfis cadastradas
        /// </summary>
        /// <returns>Lista com todos os Perfis</returns>
        [Authorize(Roles = "Professor")]
        // GET: api/<PerfilController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var perfis = _perfilRepository.Mostrar();
                if (perfis.Count == 0)
                {
                    return NoContent();
                }
                return Ok(perfis);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Mostra um único Perfil especificado pelo seu ID
        /// </summary>
        /// <param name="id">ID do Perfil</param>
        /// <returns>Um Perfil</returns>
        [Authorize(Roles = "Professor")]
        // GET api/<PerfilController>/5
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var perfil = _perfilRepository.BuscarPorId(id);
                if (perfil == null)
                {
                    return NotFound();
                }
                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///  Cadastra um novo Perfil
        /// </summary>
        /// <param name="perfil">Objeto perfil</param>
        /// <returns>Perfil Cadastrado</returns>
        [Authorize(Roles = "Professor")]
        // POST api/<PerfilController>
        [HttpPost]
        public IActionResult Post(Perfil perfil)
        {
            try
            {
                _perfilRepository.Adicionar(perfil);

                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Altera um determinado Perfil
        /// </summary>
        /// <param name="perfil">Objeto Perfil com as alterações</param>
        /// <returns>Informações alteradas do Perfil</returns>
        [Authorize(Roles = "Professor")]
        // PUT api/<PerfilController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Perfil perfil)
        {
            try
            {
                _perfilRepository.Editar(perfil);

                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Excluí um determinado Perfil
        /// </summary>
        /// <param name="id">ID do Perfil</param>
        /// <returns>ID excluído</returns>
        [Authorize(Roles = "Professor")]
        // DELETE api/<PerfilController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _perfilRepository.Remover(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
