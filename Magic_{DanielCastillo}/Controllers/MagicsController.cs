using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magic__DanielCastillo_.Data;
using Magic__DanielCastillo_.Model;

namespace Magic__DanielCastillo_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagicsController : ControllerBase
    {
        private static readonly string[] nombreide = new[]
       {
            "Bueno", "malo", "por ahi va", "amor", "setso", "muerte", "musica", "dinero", "no dinero", "mujeres"
        };

        private static readonly string[] vision = new[]
       {
            "tu futuro sera bueno", "tu futuro sera malo", "es incierto", "enontraras al amor de tu vida"
        };

        private static readonly string[] image = new[]
     {
            "https://th.bing.com/th/id/R.4c186574255a4005c67a7f102eb04c10?rik=A2hUpQF6KQp7LQ&pid=ImgRaw&r=0","https://th.bing.com/th/id/R.9e1b644d7cd89626476c1012081627db?rik=dzpaU9%2fX4QVUwg&pid=ImgRaw&r=0"
        };

        private readonly AppDbContext _context;

        public MagicsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Magics
        [HttpGet]
        public IEnumerable<Magic> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Magic
            {
                Vision = vision[rng.Next(vision.Length)],
                Imagen = image[rng.Next(image.Length)],
                FuturoId = nombreide[rng.Next(nombreide.Length)]
            })
            .ToArray();
        }

        // GET: api/Magics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Magic>> GetMagic(string id)
        {
            var magic = await _context.Magic.FindAsync(id);

            if (magic == null)
            {
                return NotFound();
            }

            return magic;
        }

        // PUT: api/Magics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagic(string id, Magic magic)
        {
            if (id != magic.FuturoId)
            {
                return BadRequest();
            }

            _context.Entry(magic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagicExists(id))
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

        // POST: api/Magics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Magic>> PostMagic(Magic magic)
        {
            _context.Magic.Add(magic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MagicExists(magic.FuturoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMagic", new { id = magic.FuturoId }, magic);
        }

        // DELETE: api/Magics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMagic(string id)
        {
            var magic = await _context.Magic.FindAsync(id);
            if (magic == null)
            {
                return NotFound();
            }

            _context.Magic.Remove(magic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MagicExists(string id)
        {
            return _context.Magic.Any(e => e.FuturoId == id);
        }
    }
}
