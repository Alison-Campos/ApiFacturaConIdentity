using API_Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Facturacion.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            
        }
       
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }

}
