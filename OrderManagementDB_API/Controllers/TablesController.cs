using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly OrderManagementDBContext _context;

        // Constructor to inject the database context
        public TablesController(OrderManagementDBContext context)
        {
            _context = context;
        }

        // GET: api/tables
        // Retrieves the list of all tables from the database
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tables>>> Get()
        {
            return await _context.Tables.ToListAsync();
        }

        // GET: api/tables/5
        // Retrieves a specific table by its ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tables>> Get(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        // POST: api/tables
        // Creates a new table in the database
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Tables>> Post(Tables table)
        {
            var newTable = new Tables
            {
                TableNumber = table.TableNumber,
                PositionX = table.PositionX,
                PositionY = table.PositionY,
                Status = table.Status,
                Capacity = table.Capacity
            };

            _context.Tables.Add(newTable);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newTable.TableID }, newTable);
        }

        // PUT: api/tables/5
        // Updates an existing table based on the ID
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tables table)
        {
            var existingTable = await _context.Tables.FindAsync(id);

            if (existingTable == null)
            {
                return NotFound();
            }

            existingTable.TableNumber = table.TableNumber;
            existingTable.PositionX = table.PositionX;
            existingTable.PositionY = table.PositionY;
            existingTable.Status = table.Status;
            existingTable.Capacity = table.Capacity;

            try
            {
                _context.Entry(existingTable).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error updating the database: {ex.Message}");
            }
        }

        // DELETE: api/tables/5
        // Deletes a table by ID
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
