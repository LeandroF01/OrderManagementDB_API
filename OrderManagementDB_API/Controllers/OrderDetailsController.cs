using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public OrderDetailsController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/orderDetails
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> Get()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/orderDetails/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> Get(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return orderDetails;
        }

        // POST: api/orderDetails
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> Post(OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = orderDetails.OrderDetailID }, orderDetails);
        }

        // PUT: api/orderDetails/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderDetails orderDetails)
        {
            if (id != orderDetails.OrderDetailID)
            {
                return BadRequest();
            }

            _context.Entry(orderDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orderDetails/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
