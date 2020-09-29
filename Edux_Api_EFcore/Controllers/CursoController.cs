using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux_Api_EFcore.Domains;
using Edux_Api_EFcore.Interfaces;
using Edux_Api_EFcore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edux_Api_EFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController()
        {
            _cursoRepository = new CursoRepository();
        }

        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<CursoController>
        [HttpGet]
        public IActionResult GetCurso()
        {
            try
            {
                var cursos = _cursoRepository.Listar();
                var qtdCursos = cursos.Count;

                if (qtdCursos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdCursos,
                    data = cursos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        [Authorize(Roles = "Professor")]
        // GET api/<CursoController>/buscar/id/5
        [HttpGet("buscar/id/{id}")]
        public IActionResult GetCurso(Guid id)
        {
            try
            {
                var curso = _cursoRepository.BuscarPorId(id);

                if (curso == null)
                    return NotFound();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com.");
            }
        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET api/<CursoController>/buscar/titulo/DesenvolvimentoDeSistemas
        [HttpGet("buscar/titulo/{titulo}")]
        public IActionResult GetCurso(string titulo)
        {
            try
            {
                var cursos = _cursoRepository.BuscarPorTitulo(titulo);
                var qtdCursos = cursos.Count;

                if (qtdCursos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdCursos,
                    data = cursos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        [Authorize(Roles = "Professor")]
        // POST api/<CursoController>
        [HttpPost]
        public IActionResult PostCurso([FromBody] Curso curso)
        {
            try
            {
                _cursoRepository.Adicionar(curso);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        [Authorize(Roles = "Professor")]
        // PUT api/<CursoController>
        [HttpPut]
        public IActionResult PutCurso([FromBody] Curso curso)
        {
            try
            {
                _cursoRepository.Editar(curso);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        [Authorize(Roles = "Professor")]
        // DELETE api/<CursoController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCurso(Guid id)
        {
            try
            {
                _cursoRepository.Remover(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }
    }
}