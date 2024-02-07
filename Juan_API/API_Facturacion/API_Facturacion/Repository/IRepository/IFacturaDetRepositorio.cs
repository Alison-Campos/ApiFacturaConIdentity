using API_Facturacion.Models;

namespace API_Facturacion.Repository.IRepository
{
    public interface IFacturaDetRepositorio
    {
        ICollection<FacturaDetalle> GetFactursDetalle();
        FacturaDetalle GetFacturaDetalle(Guid IdDetalle);
        bool ExisteFacturaDetalle(int numFactura);
        bool ExisteFacturaDetalle(Guid idIdDetalle);
        bool CrearFacturaDetalle(FacturaDetalle FacturaDetalle);
        bool ActualizarFacturaDetalle(FacturaDetalle FacturaDetalle);
        bool BorrarFacturaDetalle(FacturaDetalle FacturaDetalle);
        bool Guardar();
    }
}
