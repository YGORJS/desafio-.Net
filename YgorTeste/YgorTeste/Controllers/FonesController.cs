using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FonesController : ControllerBase
    {
        private readonly ApiContext _context;

        public FonesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Fones
        [HttpGet]
        public IEnumerable<Fone> GetFone()
        {
            return _context.Fone;
        }

        // GET: api/Fones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fone = await _context.Fone.FindAsync(id);

            if (fone == null)
            {
                return NotFound();
            }

            return Ok(fone);
        }

        // PUT: api/Fones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFone([FromRoute] int id, [FromBody] Fone fone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fone.Id)
            {
                return BadRequest();
            }

            _context.Entry(fone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoneExists(id))
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

        // POST: api/Fones
        [HttpPost]
        public async Task<IActionResult> PostFone([FromBody] Fone fone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fone.Add(fone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFone", new { id = fone.Id }, fone);
        }

        // DELETE: api/Fones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fone = await _context.Fone.FindAsync(id);
            if (fone == null)
            {
                return NotFound();
            }

            _context.Fone.Remove(fone);
            await _context.SaveChangesAsync();

            return Ok(fone);
        }

        private bool FoneExists(int id)
        {
            return _context.Fone.Any(e => e.Id == id);
        }
    }
}