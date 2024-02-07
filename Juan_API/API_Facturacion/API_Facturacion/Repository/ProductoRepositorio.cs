using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Facturacion.Repository
{
    
    public class ProductoRepositorio: IProductoRepositorio
    {
        private readonly Context _context;
        public ProductoRepositorio(Context context)
        {
            _context = context;
        }

        public bool ActualizarProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            return Guardar();
        }

        public bool BorrarProducto(Producto producto)
        {
            _context.Productos.Remove(producto);
            return Guardar();
        }

        public bool CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            return Guardar();
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _context.Productos.Any(c=> c.nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }
        public bool ExisteProducto(Guid id)
        {
            bool valor = _context.Productos.Any(c => c.idProducto == id);
            return valor;
        }
        public Producto GetProducto(Guid id)
        {
           return _context.Productos.FirstOrDefault(p => p.idProducto ==id);
        }
        

        public ICollection<Producto> GetProductos()
        {
            return _context.Productos.OrderBy(p => p.nombre).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
