using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public PaymentMethodsController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/paymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethods>>> Get()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        // GET: api/paymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethods>> Get(int id)
        {
            var paymentMethods = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethods == null)
            {
                return NotFound();
            }
            return paymentMethods;
        }

        // POST: api/paymentMethods
        [HttpPost]
        public async Task<ActionResult<PaymentMethods>> Post(PaymentMethods paymentMethods)
        {
            _context.PaymentMethods.Add(paymentMethods);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = paymentMethods.PaymentID }, paymentMethods);
        }

        // PUT: api/paymentMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PaymentMethods paymentMethods)
        {
            if (id != paymentMethods.PaymentID)
            {
                return BadRequest();
            }

            _context.Entry(paymentMethods).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/paymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paymentMethods = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethods == null)
            {
                return NotFound();
            }

            _context.PaymentMethods.Remove(paymentMethods);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
