using Intranet_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intranet_API.Controllers;

[ApiController]
[Route("[controller]/")]
public class AuthController(IntranetDbContext dbContext) : ControllerBase {

    IntranetDbContext _dbContext = dbContext;

    [HttpGet("token/validate")]
    [Authorize]
    public async Task<IActionResult> ValidateToken() {
        return Ok();
    }
}