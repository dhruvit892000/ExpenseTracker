using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exptracker2.Data;
using Exptracker2.Models;

namespace Exptracker2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalExplimsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TotalExplimsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TotalExplims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TotalExplim>>> GetTotalExplims()
        {
            return await _context.TotalExplims.ToListAsync();
        }

        // GET: api/TotalExplims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TotalExplim>> GetTotalExplim(int id)
        {
            var totalExplim = await _context.TotalExplims.FindAsync(id);

            if (totalExplim == null)
            {
                return NotFound();
            }

            return totalExplim;
        }

        // PUT: api/TotalExplims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTotalExplim(int id, TotalExplim totalExplim)
        {
            if (id != totalExplim.ExpLimit_Id)
            {
                return BadRequest();
            }

            _context.Entry(totalExplim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalExplimExists(id))
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

        // POST: api/TotalExplims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TotalExplim>> PostTotalExplim(TotalExplim totalExplim)
        {
            _context.TotalExplims.Add(totalExplim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTotalExplim", new { id = totalExplim.ExpLimit_Id }, totalExplim);
        }

        // DELETE: api/TotalExplims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTotalExplim(int id)
        {
            var totalExplim = await _context.TotalExplims.FindAsync(id);
            if (totalExplim == null)
            {
                return NotFound();
            }

            _context.TotalExplims.Remove(totalExplim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TotalExplimExists(int id)
        {
            return _context.TotalExplims.Any(e => e.ExpLimit_Id == id);
        }
    }
}
