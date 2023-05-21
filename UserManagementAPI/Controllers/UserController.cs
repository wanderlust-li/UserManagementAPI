
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
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var result = _context.Users.FirstOrDefault(u => u.Id == id);
        if (result == null)
            return NotFound();

        return result;
    }
    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
    [HttpDelete]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var result = _context.Users.FirstOrDefault(u => u.Id == id);
        if (result == null)
            return NotFound();
        _context.Users.Remove(result);
        await _context.SaveChangesAsync();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(user);

    }
}