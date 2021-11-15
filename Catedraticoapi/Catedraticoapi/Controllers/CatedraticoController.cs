using Catedraticoapi.Data;
using Catedraticoapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catedraticoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatedraticoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private object _context;

        public CatedraticoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatedraticos()
        {
            var lista = await _db.Catedraticos.OrderBy(c => c.Nombre).ToListAsync();

            return Ok(lista);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCatedratico(int id)
        {
            var obj = await _db.Catedraticos.FirstOrDefaultAsync(c => c.Idcatedratico == id);

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCatedratico([FromBody] Catedratico catedratico)
        {
            if (catedratico == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _db.AddAsync(catedratico);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCatedraticos(int id)
        {
            var catedratico = await _db.Catedraticos.FindAsync(id);
            if(catedratico == null)
            {
                return NotFound();
            }
            _db.Catedraticos.Remove(catedratico);
            await _db.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCatedraticos(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var catedra = _db.Catedraticos.First(x => x.Idcatedratico == id);
            catedra.Nombre = catedra.Nombre;
            catedra.Apellido = catedra.Apellido;
            catedra.Edad = catedra.Edad;
            catedra.Estado = catedra.Estado;

            _db.Catedraticos.Update(catedra);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatedraticoExists(id))
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
       
        private bool CatedraticoExists(int id)
        {
            return _db.Catedraticos.Any(e => e.Idcatedratico == id);
        }


    }
}
