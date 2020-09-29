using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edux_Api_EFcore.Contexts;
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
    
    public class CategoriaController : ControllerBase
    {
       

        private readonly ICategoriaRepository _categoriaRepository;
        
        public CategoriaController()
        {

            _categoriaRepository = new CategoriaRepository();

        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET: api/<CategoriaController>
        [HttpGet]
        public IActionResult Get()
        {
       
            try
            {
                             
                var categorias = _categoriaRepository.Listar();
                var qtdCategorias = categorias.Count;

                if (qtdCategorias == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdCategorias,
                    data = categorias
                }) ;


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }
        }


        [Authorize(Roles = "Professor")]
        // GET api/<CategoriaController>/listar/id = 2
        [HttpGet("{id}")]
        public IActionResult Get(Guid Id)
        {
            try
            {
                var categoria = _categoriaRepository.BuscarPorId(Id);

                if (categoria == null)
                    return NotFound();

                return Ok(categoria);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }


        }


        [Authorize(Roles = "Aluno,Professor")]
        // GET api/<CategoriaController>/listar/tipo = critico
        [HttpGet("{id}")]
        public IActionResult Get(string tipo)
        {

            try
            {

                var categorias = _categoriaRepository.BuscarPorTipo(tipo);
                var qtdCategorias = categorias.Count;

                if (qtdCategorias == 0)
                    return NoContent();

                return Ok(new
                {
                    totalCount = qtdCategorias,
                    data = categorias
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }


        [Authorize(Roles = "Professor")]
        // POST api/<CategoriaController>
        [HttpPost]
        public IActionResult Post([FromBody] Categoria categoria)
        {

            try
            {
                   
                categoria = _categoriaRepository.Adicionar(categoria);
                return Ok(categoria);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");


            }


        }


        [Authorize(Roles = "Professor")]
        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Categoria categoria)
        {
            try
            {
                _categoriaRepository.Editar(categoria);

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }


        }


        [Authorize(Roles = "Professor")]
        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {

            try
            {
                _categoriaRepository.Remover(Id);


                return Ok(Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + ". contate nossa equipe de suporte para solucionarmos o erro presente nesta página email : edux.suporte@gmail.com, telefone : (11)31212121");

            }

        }

    }
}
