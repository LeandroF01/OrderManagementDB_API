using DB;
using Microsoft.IdentityModel.Tokens;
using OrderManagementDB_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace OrderManagementDB_API.Services
{
    public class AuthService
    {
        private  OrderManagementDBContext _context;
        private  IConfiguration _configuration;

        public AuthService(OrderManagementDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(LoginModel loginModel)
        {
            // Validar las credenciales
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginModel.Email);
            if (user == null)
            {
                Console.WriteLine($"User with email {loginModel.Email} not found.");
                return "User not found.";
            }

            if (!VerifyPassword(loginModel.Password, user.Password))
            {
                Console.WriteLine($"Incorrect password for user {loginModel.Email}.");
                return "Incorrect password.";
            }

            Console.WriteLine("Authentication successful.");

            // Generar el token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.UserID.ToString()),
                new Claim(ClaimTypes.Role, user.UserType)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPassword(string password, string storedHashedPassword)
        {
            string hashed = HashPassword(password);
            return hashed == storedHashedPassword;
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
