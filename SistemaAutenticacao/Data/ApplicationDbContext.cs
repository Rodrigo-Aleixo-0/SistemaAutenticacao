using Microsoft.EntityFrameworkCore;
using SistemaAutenticacao.Models;

namespace SistemaAutenticacao.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UsuariosModel> Usuarios { get; set; }

    }
}
