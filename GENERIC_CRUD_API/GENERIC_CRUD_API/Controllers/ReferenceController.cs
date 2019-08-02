using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GENERIC_CRUD_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GENERIC_CRUD_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {

        private readonly ReferenceContext _context;
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}


        public ReferenceController(ReferenceContext context)
        {
            _context = context;

            if (_context.ReferenceItems.Count() == 0)
            {
                // Create a new ReferenceModel if collection is empty,
                // which means you can't delete all ReferenceItems.
                _context.ReferenceItems.Add(new ReferenceModel { Name = "Reference Name 1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceModel>>> GetReferenceItems()
        {
            return await _context.ReferenceItems.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReferenceModel>> GetReferenceItems(int id)
        {
            var referenceItem = await _context.ReferenceItems
                .FindAsync(id);

            if (referenceItem == null)
            {
                return NotFound();
            }

            return referenceItem;
        }

        // POST: api/Reference
        [HttpPost]
        public async Task<ActionResult<ReferenceModel>> PostReferenceItem(ReferenceModel referenceItem)
        {
            _context.ReferenceItems.Add(referenceItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReferenceItems), new { id = referenceItem.Id }, referenceItem);
        }

        // PUT: api/Reference/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferenceItem(int id, ReferenceModel referenceItem)
        {
            if (id != referenceItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(referenceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferenceItem(int id)
        {
            var todoItem = await _context.ReferenceItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ReferenceItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
