using DB;
using DB.DTO;
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

        // Constructor to inject the database context
        public OrdersController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/orders
        // Retrieves the list of all orders from the database
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/orders/5
        // Retrieves a specific order by its ID
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
        // Creates a new order in the database
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Orders>> Post(Orders order)
        {
            var orders = new Orders
            {
                UserID = order.UserID,
                Date = order.Date,
                Status = order.Status,
                OrderType = order.OrderType,
                Total = order.Total
            };

            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = orders.OrderID }, orders);
        }

        // PUT: api/orders/5
        // Updates an existing order based on the ID
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Orders orders)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.UserID = orders.UserID;
            order.Date = orders.Date;
            order.Status = orders.Status;
            order.OrderType = orders.OrderType;
            order.Total = orders.Total;

            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error actualizando la base de datos: {ex.Message}");
            }
        }

        // DELETE: api/orders/5
        // Deletes an order by ID
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
