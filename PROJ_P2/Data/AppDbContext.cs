using Microsoft.EntityFrameworkCore;
using PROJ_P2.Models;

namespace PROJ_P2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Carro> Carros { get; set; }
    public DbSet<Mecanico> Mecanicos { get; set; }
    public DbSet<OrdemServico> OrdensServico { get; set; }
}