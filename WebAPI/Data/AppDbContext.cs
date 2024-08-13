namespace WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AutorModel> Autores { get; set; }

    public DbSet<LivroModel> Livros { get; set; }
   
}
