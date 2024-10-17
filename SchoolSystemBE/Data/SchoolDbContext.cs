using Microsoft.EntityFrameworkCore;
using SchoolSystemBE.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Grades> Grades { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kol kas nereikia
    }
}
