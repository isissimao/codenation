using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models
{
    public class Contexto : IdentityDbContext
    {
        public Contexto (DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Prato> Prato { get; set; }
    }
}
