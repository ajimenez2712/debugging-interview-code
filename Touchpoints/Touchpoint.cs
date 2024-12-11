using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Touchpoint
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}

public class ApplicationDbContext : DbContext
{
  public DbSet<Touchpoint> Touchpoints { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }
}
