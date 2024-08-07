using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public AddressesController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/addresses
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addresses>>> Get()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/addresses/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Addresses>> Get(int id)
        {
            var addresses = await _context.Addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }
            return addresses;
        }

        // POST: api/addresses
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Addresses>> Post(Addresses addresses)
        {
            _context.Addresses.Add(addresses);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = addresses.AddressID }, addresses);
        }

        // PUT: api/orderDetails/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Addresses addresses)
        {
            if (id != addresses.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(addresses).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orderDetails/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var addresses = await _context.Addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addresses);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
