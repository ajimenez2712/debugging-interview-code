using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Touchpoint
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public DateTime DateCreated { get; set; }
}

public class ApplicationDbContext : DbContext
{
  public DbSet<Touchpoint> Touchpoints { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }
}

[Route("api/touchpoints")]
public class TouchpointsController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public TouchpointsController(ApplicationDbContext context)
  {
    _context = context;

    // Add sample data
    if (!_context.Touchpoints.Any())
    {
      _context.Touchpoints.Add(new Touchpoint
      {
        Name = "Initial Touchpoint",
        Description = "This is a sample touchpoint.",
        DateCreated = DateTime.Now
      });
      _context.SaveChanges();
    }
  }

  // BUG: This method throws an exception.
  [HttpGet]
  public async Task<ActionResult<List<Touchpoint>>> GetTouchpoints()
  {
    var touchpoints = await _context.Touchpoints.ToListAsync();
    if (touchpoints == null)
    {
      return NotFound("No touchpoints found");
    }
    return touchpoints;
  }

  // BUG: This method fails to insert a touchpoint correctly.
  [HttpPost]
  public async Task<ActionResult> AddTouchpoint([FromBody] Touchpoint touchpoint)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    // Missing important fields or incorrect EF operations
    _context.Touchpoints.Add(touchpoint);
    await _context.SaveChangesAsync();
    return Ok();
  }
}
