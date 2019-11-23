using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Restaurante.Controllers
{
    /// <summary>
    /// pratos do restaurante
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PratoController : ControllerBase
    {
        private readonly Contexto _context;

        public PratoController(Contexto context)
        {
            _context = context;
        }

        private bool PratoExiste(int id)
        {
            return _context.Prato.Any(e => e.Id == id);
        }

        /// <summary>
        /// Retorna os pratos cadastrados
        /// </summary>
        /// <remarks>retorna uma lista de pratos</remarks>
        /// <response code="200">sucesso na requisição</response>
        /// <response code="400">requisição inválida</response>
        [HttpGet]
        //[ProducesResponseType(200)] //forma nativa do asp.net core
        //[ProducesResponseType(400)]
        public virtual IActionResult Get()
        {
            try
            {
                var pratos = _context.Prato.ToList();
                return Ok(pratos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var prato = _context.Prato.Find(id);

                if (prato == null)
                {
                    return NotFound();
                }

                return Ok(prato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        //[ClaimsAuthorize("Prato", "Incluir")]
        public IActionResult Incluir(Prato prato)
        {
            try
            {
                _context.Prato.Add(prato);
                _context.SaveChanges();

                return CreatedAtAction("Incluir", new { id = prato.Id }, prato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        //[ClaimsAuthorize("Prato", "Editar")]
        public IActionResult Atualizar(int id, Prato prato)
        {
            if (id != prato.Id)
            {
                return BadRequest();
            }

            _context.Entry(prato).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PratoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[ClaimsAuthorize("Prato", "Excluir")]
        public IActionResult Apagar(int id)
        {
            var prato = _context.Prato.Find(id);
            if (prato == null)
            {
                return NotFound();
            }

            _context.Prato.Remove(prato);
            _context.SaveChanges();

            return Ok(prato);
        }
    }
}
