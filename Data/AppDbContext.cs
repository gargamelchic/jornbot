using Microsoft.EntityFrameworkCore;
using tggargamel.Models;

namespace tggargamel.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MyStudentDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
