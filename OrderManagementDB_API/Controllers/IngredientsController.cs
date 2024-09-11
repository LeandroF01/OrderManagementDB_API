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
    public class IngredientsController : ControllerBase
    {

        private OrderManagementDBContext _context;

        public IngredientsController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredients>>> Get()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // GET: api/ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredients>> Get(int id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }
            return ingredients;
        }

        // POST: api/ingredients
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Ingredients>> Post(IngredientsDTO ingredientsDto)
        {
            var ingredients = new Ingredients
            {
                Name = ingredientsDto.Name,
                Quantity = ingredientsDto.Quantity,
                Unit = ingredientsDto.Unit
            };

            _context.Ingredients.Add(ingredients);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = ingredients.IngredientID }, ingredients);
        }

        // PUT: api/ingredients/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ingredients ingredients)
        {
            if (id != ingredients.IngredientID)
            {
                return BadRequest();
            }

            _context.Entry(ingredients).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ingredients/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
