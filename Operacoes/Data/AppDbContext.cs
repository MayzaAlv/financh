using Microsoft.EntityFrameworkCore;
using Operacoes.Models;

namespace Operacoes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Salario> Salarios { get; set; }
    }
}
