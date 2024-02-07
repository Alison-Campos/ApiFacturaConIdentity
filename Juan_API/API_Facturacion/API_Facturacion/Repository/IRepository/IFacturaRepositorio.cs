using API_Facturacion.Models;

namespace API_Facturacion.Repository.IRepository
{
    public interface IFacturaRepositorio
    {
        ICollection<Factura> GetFacturas();
        Factura GetFactura(int id);
        bool ExisteFactura(string nombre);
        bool ExisteFactura(int id);
        bool CrearFactura(Factura fact);
        bool ActualizarFactura(Factura fact);
        bool BorrarFactura(Factura fact);
        bool Guardar();
    }
}
