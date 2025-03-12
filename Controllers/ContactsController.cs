using Intranet_API.Models;
using Intranet_API.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet_API.Controllers;

[ApiController]
[Route("[controller]/")]
[Authorize]
public class ContactsController(IntranetDbContext dbContext) : ControllerBase {
    IntranetDbContext _dbContext = dbContext;

    [HttpGet("fetch/all")]
    public async Task<IActionResult> FetchAllContacts() {
        List<User> allUsers = await _dbContext.Users.ToListAsync();
        
        // Filter out security info such as username, password, etc.
        List<Profile> contacts = new List<Profile>();
        foreach(User user in allUsers) {
            contacts.Add(new Profile() {
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Department = user.Department,
                JobTitle = user.JobTitle
            });
        }

        return Ok(contacts);
    }
}