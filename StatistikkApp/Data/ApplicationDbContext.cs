using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StatistikkApp.Models;

namespace StatistikkApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Kommune> Kommuner { get; set; }
    public DbSet<StatistikkData> StatistikkData { get; set; }
    public DbSet<StatistikkKategori> StatistikkKategorier { get; set; }
}