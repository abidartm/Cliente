using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cliente2.Data;
using Cliente2.Models;


namespace Cliente2.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ModelClientesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ModelClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ModelCliente>> GetClientes()
        {
            return _context.cliente.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ModelCliente>> PostCliente(ModelCliente cliente)
        {
            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();

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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ModelClienteExists(int id)
        {
            return _context.cliente.Any(e => e.IdCliente == id);
        }
    }
}
