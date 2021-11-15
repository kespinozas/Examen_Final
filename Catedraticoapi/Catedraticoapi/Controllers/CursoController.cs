using Catedraticoapi.Data;
using Catedraticoapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedraticoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private object _context;

        public CursoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CrearNota([FromBody] Curso curso)
        {
            if (curso == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _db.AddAsync(curso);
            await _db.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarNotas(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var nota = _db.Cursos.First(x => x.Idcurso == id);
            nota.Nombre = nota.Nombre;
            nota.Punteo = nota.Punteo;

            _db.Cursos.Update(nota);

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
