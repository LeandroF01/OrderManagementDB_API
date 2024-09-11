using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DB.DTO;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductIngredientsController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public ProductIngredientsController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/productIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductIngredients>>> Get()
        {
            return await _context.ProductIngredients.ToListAsync();
        }

        // GET: api/productIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductIngredients>> Get(int id)
        {
            var productIngredients = await _context.ProductIngredients.FindAsync(id);
            if (productIngredients == null)
            {
                return NotFound();
            }
            return productIngredients;
        }

        // POST: api/productIngredients
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductIngredients>> Post(ProductIngredientsDTO productIngredientsDto)
        {
            var productIngredients = new ProductIngredients
            {             
               ProductID = productIngredientsDto.ProductID,
               IngredientID = productIngredientsDto.IngredientID,
               Quantity = productIngredientsDto.Quantity,
            };
            _context.ProductIngredients.Add(productIngredients);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = productIngredients.ProductIngredientID }, productIngredients);
        }

        // PUT: api/productIngredients/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductIngredients productIngredients)
        {
            if (id != productIngredients.ProductIngredientID)
            {
                return BadRequest();
            }

            _context.Entry(productIngredients).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/productIngredients/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productIngredients = await _context.ProductIngredients.FindAsync(id);
            if (productIngredients == null)
            {
                return NotFound();
            }

            _context.ProductIngredients.Remove(productIngredients);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
