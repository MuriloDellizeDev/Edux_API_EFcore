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
    public class ObjetivoAlunoController : ControllerBase
    {
        private readonly IObjetivoAlunoRepository _objetivoAlunoRepository;

        public ObjetivoAlunoController()
        {
            _objetivoAlunoRepository = new ObjetivoAlunoRepository();
        }

        // GET: api/<ObjetivoAlunoController>
        [Authorize(Roles = "Aluno,Professor")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var objetivosAlunos = _objetivoAlunoRepository.Buscar();
                var qtdObjetivosAlunos = objetivosAlunos.Count;

                if (qtdObjetivosAlunos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdObjetivosAlunos,
                    data = objetivosAlunos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        // GET api/<ObjetivoAlunoController>/buscar/id/5
        [Authorize(Roles = "Professor")]
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var objetivoAluno = _objetivoAlunoRepository.Buscar(id);

                if (objetivoAluno == null)
                    return NotFound();

                return Ok(objetivoAluno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com.");
            }
        }

        // GET api/<ObjetivoAlunoController>/buscar/id/5
        [Authorize(Roles = "Aluno,Professor")]
        [HttpGet("buscar/por_aluno/{idAluno}")]
        public IActionResult GetPorAluno(Guid idAluno)
        {
            try
            {
                var objetivosAlunos = _objetivoAlunoRepository.BuscarPorAluno(idAluno);
                var qtdObjetivosAlunos = objetivosAlunos.Count;

                if (qtdObjetivosAlunos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdObjetivosAlunos,
                    data = objetivosAlunos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // POST api/<ObjetivoAlunoController>
        [Authorize(Roles = "Professor")]
        [HttpPost]
        public IActionResult Post([FromBody] ObjetivoAluno objetivoAluno)
        {
            try
            {
                _objetivoAlunoRepository.Cadastrar(objetivoAluno);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // PUT api/<ObjetivoAlunoController>
        [Authorize(Roles = "Professor")]
        [HttpPut]
        public IActionResult Put([FromBody] ObjetivoAluno objetivoAluno)
        {
            try
            {
                _objetivoAlunoRepository.Alterar(objetivoAluno);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        // DELETE api/<ObjetivoAlunoController>/5
        [Authorize(Roles = "Professor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (_objetivoAlunoRepository.Buscar(id) == null)
                    return NotFound();

                _objetivoAlunoRepository.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }
    }
}