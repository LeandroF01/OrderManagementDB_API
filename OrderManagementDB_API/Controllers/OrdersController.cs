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
        public async Task<ActionResult<Orders>> Post(OrdersDTO ordersDto)
        {
            var orders = new Orders
            {
                UserID = ordersDto.UserID,
                Date = ordersDto.Date,
                Status = ordersDto.Status,
                OrderType = ordersDto.OrderType,
                Total = ordersDto.Total
            };

            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = orders.OrderID }, orders);
        }

        // PUT: api/orders/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Orders orders)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del pedido con los nuevos valores
            order.UserID = orders.UserID;
            order.Date = orders.Date;
            order.Status = orders.Status;
            order.OrderType = orders.OrderType;
            order.Total = orders.Total;

            try
            {
                // Guardar cambios
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Devolver 204 NoContent si todo salió bien
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Manejar posibles errores en la actualización
                return StatusCode(500, $"Error actualizando la base de datos: {ex.Message}");
            }
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
