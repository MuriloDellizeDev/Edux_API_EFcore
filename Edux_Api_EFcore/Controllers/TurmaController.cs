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
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaController()
        {
            _turmaRepository = new TurmaRepository();
        }

        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<TurmaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var turmas = _turmaRepository.Listar();
                var qtdTurmas = turmas.Count;

                if (qtdTurmas == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdTurmas,
                    data = turmas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }

        [Authorize(Roles = "Professor")]
        // GET api/<TurmaController>/buscar/id/5
        [HttpGet("buscar/id/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var turma = _turmaRepository.BuscarPorId(id);

                if (turma == null)
                    return NotFound();

                return Ok(turma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com.");
            }
        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<TurmaController>/buscar/nome/1DT
        [HttpGet("buscar/nome/{nome}")]
        public IActionResult Get(string nome)
        {
            try
            {
                var turmas = _turmaRepository.BuscarPorNome(nome);
                var qtdTurmas = turmas.Count;

                if (qtdTurmas == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdTurmas,
                    data = turmas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        [Authorize(Roles = "Professor")]
        // POST api/<TurmaController>
        [HttpPost]
        public IActionResult Post([FromBody] Turma turma, [FromBody] List<ProfessorTurma> professores, [FromBody] List<AlunoTurma> alunos)
        {
            try
            {
                _turmaRepository.Adicionar(turma, professores, alunos);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        [Authorize(Roles = "Professor")]
        // PUT api/<TurmaController>
        [HttpPut]
        public IActionResult Put([FromBody] Turma turma, List<ProfessorTurma> professores, List<AlunoTurma> alunos)
        {
            try
            {
                _turmaRepository.Editar(turma, professores, alunos);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }


        [Authorize(Roles = "Professor")]
        // DELETE api/<TurmaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _turmaRepository.Remover(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". Envie um email para a nossa equipe de suporte: edux.suport@gmail.com");
            }
        }
    }
}
