using API_Facturacion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Facturacion.Data
{
    public class Context: IdentityDbContext<AppUsuario>
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AppUsuario> AppUsuario { get; set; }
    }

}
