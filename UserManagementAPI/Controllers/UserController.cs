
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    // 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return await _context.Users.ToListAsync();
    }
}