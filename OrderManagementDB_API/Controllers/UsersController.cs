using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace OrderManagementDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private OrderManagementDBContext _context;

        public UsersController(OrderManagementDBContext context)
        {
            _context = context;
        }
        //cambio
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<Users>> Post(Users users)
        {
            // Hashear la contraseña antes de guardarla
            users.Password = HashPassword(users.Password);

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = users.UserID }, users);
        }

        // PUT: api/users/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Users users)
        {
            if (id != users.UserID)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(users.Password))
            {
                users.Password = HashPassword(users.Password);
            }

                _context.Entry(users).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/5

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string HashPassword(string password)
        {

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[0], // Sin usar salt, se pasa un array vacío
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }


    }
}
