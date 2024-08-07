using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public OrdersController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/orders/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> Get(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return orders;
        }

        // POST: api/orders
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Orders>> Post(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = orders.OrderID }, orders);
        }

        // PUT: api/orders/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Orders orders)
        {
            if (id != orders.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orders/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
