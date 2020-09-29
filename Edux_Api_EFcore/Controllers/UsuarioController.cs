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
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _usuarioRepository.Mostrar();
                if (usuarios.Count == 0)
                {
                    return NoContent();
                }
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Professor")]
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var usuario = _usuarioRepository.BuscarPorId(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET api/<ObjetivoController>/
        [HttpGet("{id}")]
        public IActionResult Get(string nome)
        {
            try
            {

                var usuarios = _usuarioRepository.BuscarPorNome(nome);
                var qtdUsuarios = usuarios.Count;

                if (qtdUsuarios == 0)
                {
                    return NoContent();
                }                     

                return Ok(new
                {
                    totalCount = qtdUsuarios,
                    data = usuarios
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }


        [Authorize(Roles = "Professor")]
        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Adicionar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Professor")]
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioRepository.Editar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Professor")]
        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _usuarioRepository.Remover(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
