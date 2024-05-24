using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente2.Data;
using Cliente2.Models;
using Microsoft.Extensions.Logging;


namespace Cliente2.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ModelClientesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContext> _logger;

        //public ModelClientesController(ApplicationDbContext context)
        //{
        //_context = context;
        //}
        public ModelClientesController(ApplicationDbContext context, ILogger<ApplicationDbContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ModelCliente>> GetClientes()
        {
            IEnumerable<ModelCliente> vrLista = new List<ModelCliente>();   
            _logger.LogInformation("Obteniendo Todos los Clientes");
            //aca boy
            try
            {
                vrLista = _context.cliente;
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogInformation("Error de excepciòn");
            }
            
            return vrLista.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ModelCliente>> PostCliente(ModelCliente cliente)
        {
            try
            {
                _context.cliente.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogInformation("Error de excepciòn");
            }

            return CreatedAtAction(nameof(GetClientes), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ModelCliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelClienteExists(id))
                {
                    _logger.LogInformation("Error de excepciòn");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("Error de excepciòn");
                    throw;
                }
            }

            return NoContent();
        }

        private bool ModelClienteExists(int id)
        {
            bool exists = false;
            try
            {
                exists = _context.cliente.Any(e => e.IdCliente == id);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogInformation("Error de excepciòn");
            }

            return exists;
        }
    }
}
