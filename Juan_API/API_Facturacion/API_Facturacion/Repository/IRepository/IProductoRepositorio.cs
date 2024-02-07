using API_Facturacion.Models;

namespace API_Facturacion.Repository.IRepository
{
    public interface IProductoRepositorio
    {
        ICollection<Producto> GetProductos();
        Producto GetProducto(Guid id);
        bool ExisteProducto(string nombre);
        bool ExisteProducto(Guid id);
        bool CrearProducto(Producto producto);
        bool ActualizarProducto(Producto producto);
        bool BorrarProducto(Producto producto);
        bool Guardar();

    }
}
