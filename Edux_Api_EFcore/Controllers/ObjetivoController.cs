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
    public class ObjetivoController : ControllerBase
    {

        private readonly IObjetivoRepository _objetivoRepository;

        public ObjetivoController()
        {

            _objetivoRepository = new ObjetivoRepository();

        }

        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<ObjetivoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var objetivos = _objetivoRepository.Listar();
                var qtdObjetivos = objetivos.Count;

                if (qtdObjetivos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdObjetivos,
                    data = objetivos
                });


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }
        }


        [Authorize(Roles = "Professor")]
        // GET api/<ObjetivoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid Id)
        {
            try
            {
                var objetivo = _objetivoRepository.BuscarPorId(Id);

                if (objetivo == null)
                    return NotFound();

                return Ok(objetivo);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }
        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET api/<ObjetivoController>/
        [HttpGet("{id}")]
        public IActionResult Get(string termo)
        {

            try
            {

                var objetivos = _objetivoRepository.BuscarPorTermo(termo);
                var qtdObjetivos = objetivos.Count;

                if (qtdObjetivos == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdObjetivos,
                    data = objetivos
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }


        [Authorize(Roles = "Professor")]
        // POST api/<ObjetivoController>
        [HttpPost]
        public IActionResult Post([FromBody] Objetivo objetivo)
        {

            try
            {

                objetivo = _objetivoRepository.Adicionar(objetivo);
                return Ok(objetivo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");


            }

        }


        [Authorize(Roles = "Professor")]
        // PUT api/<ObjetivoController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Objetivo objetivo)
        {

            try
            {
                _objetivoRepository.Editar(objetivo);

                return Ok(objetivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }


        [Authorize(Roles = "Professor")]
        // DELETE api/<ObjetivoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {

            try
            {
                _objetivoRepository.Remover(Id);


                return Ok(Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }


    }
}
