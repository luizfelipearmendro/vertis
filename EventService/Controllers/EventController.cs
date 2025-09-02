using EventService.Data;
using EventService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventModel>>> Get([FromQuery] int? employeeId)
        {
            var query = _context.Events.AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(e => e.EmployeeId == employeeId.Value);

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventModel>> Get(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events == null) return NotFound();
            return events;
        }

        [HttpPost]
        public async Task<ActionResult<EventModel>> Post(EventModel events)
        {
            _context.Events.Add(events);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = events.Id }, events);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventModel events)
        {
            if (id != events.Id) return BadRequest();
            _context.Entry(events).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events == null) return NotFound();
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
