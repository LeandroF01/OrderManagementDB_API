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
    public class CategoriesController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public CategoriesController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> Get(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return categories;
        }

        // POST: api/categories
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Categories>> Post(CategoriesDTO categoriesDto)
        {
            try
            {
                var categories = new Categories
                {
                    Name = categoriesDto.Name
                };

                _context.Categories.Add(categories);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = categories.CategoryID }, categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al añadir el categorie: {ex.Message}" });
            }
        }


        // PUT: api/orderDetails/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Categories categories)
        {
            if (id != categories.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orderDetails/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
