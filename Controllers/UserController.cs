using Intranet_API.Models;
using Intranet_API.Models.Data;
using Intranet_API.Models.Requests;
using Intranet_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet_API.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class UserController(IntranetDbContext dbContext) : ControllerBase
    {
        IntranetDbContext IntranetDbContext = dbContext;
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUser form)
        {
            string name = $"{form.Forename[0].ToString().ToLower()}{form.Surname.ToString().ToLower()}";

            Console.WriteLine($"[{DateTime.UtcNow}] INFO: Searching for user with username '{name}'...");
            User? exisitingUser = await IntranetDbContext.Users.Where(user => user.Username == name).FirstOrDefaultAsync();

            if (exisitingUser != null)
            {
                return BadRequest("User already exists!");
            }
            else
            {
                Console.WriteLine($"[{DateTime.UtcNow}] INFO: Creating user with username '{name}'...");

                PasswordHasher hasher = new PasswordHasher();
                string hashedPassword = hasher.HashPassword(form.Password);
                User newUser = new User()
                {
                    Forename = form.Forename,
                    Surname = form.Surname,
                    Password = hashedPassword,
                    Username = name
                };

                try
                {
                    await IntranetDbContext.Users.AddAsync(newUser);
                    await IntranetDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.UtcNow}] ERROR: {ex.Message}");
                    return BadRequest(ex.Message);
                }

                return Ok();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromForm] LoginUser form)
        {

            Console.WriteLine($"[{DateTime.UtcNow}] INFO: Searching for user with username '{form.Username}'...");
            User? exisitingUser = await IntranetDbContext.Users.Where(user => user.Username == form.Username).FirstOrDefaultAsync();

            if (exisitingUser == null)
            {
                return BadRequest("User does not exist!");
            }
            else
            {
                Console.WriteLine($"[{DateTime.UtcNow}] INFO: Found user with username '{form.Username}'...");

                PasswordHasher hasher = new PasswordHasher();
                bool match = hasher.VerifyPassword(form.Password, exisitingUser.Password);

                if (match) {
                    try
                    {
                        exisitingUser.LastLogin = DateTime.Now;
                        await IntranetDbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{DateTime.UtcNow}] ERROR: {ex.Message}");
                        return BadRequest(ex.Message);
                    }
                    
                    Profile userProfile = new Profile() {
                        Username = exisitingUser.Username,
                        Forename = exisitingUser.Forename,
                        Surname = exisitingUser.Surname,
                    };

                    string token = JwtGenerator.GenerateJwt(userProfile);
                    return Ok(new {
                        Profile = userProfile,
                        Token = token
                    });
                }
                else
                {
                    return Unauthorized("Login details are incorrect!");
                }
            }
        }
    }
}
