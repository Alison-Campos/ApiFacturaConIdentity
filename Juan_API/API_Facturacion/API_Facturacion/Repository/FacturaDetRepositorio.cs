using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Repository.IRepository;

namespace API_Facturacion.Repository
{
    public class FacturaDetRepositorio: IFacturaDetRepositorio
    {
        private readonly Context db;
        public FacturaDetRepositorio(Context _db)
        {
            db = _db;
        }

        public bool ActualizarFacturaDetalle(FacturaDetalle FacturaDetalle)
        {
            db.FacturaDetalle.Update(FacturaDetalle);
            return Guardar();
        }

        public bool BorrarFacturaDetalle(FacturaDetalle FacturaDetalle)
        {
            db.FacturaDetalle.Remove(FacturaDetalle);
            return Guardar();
        }

        public bool CrearFacturaDetalle(FacturaDetalle FacturaDetalle)
        {
            db.FacturaDetalle.Add(FacturaDetalle);
            return Guardar();
        }

        public bool ExisteFacturaDetalle(int numFactura)
        {
            bool exist = db.FacturaDetalle.Any(f=>f.numeroFactura == numFactura);
            return exist;
        }

        public bool ExisteFacturaDetalle(Guid idIdDetalle)
        {
            bool exist = db.FacturaDetalle.Any(f=> f.IdDetalle == idIdDetalle);
            return exist;
        }

        public FacturaDetalle GetFacturaDetalle(Guid IdDetalle)
        {
            FacturaDetalle facturaD = db.FacturaDetalle.FirstOrDefault(f=>f.IdDetalle==IdDetalle);
            return facturaD;
        }

        public ICollection<FacturaDetalle> GetFactursDetalle()
        {
            var lista = db.FacturaDetalle.OrderBy(f=>f.numeroFactura).ToList();
            return lista;   
        }

        public bool Guardar()
        {
            return db.SaveChanges() >= 0? true: false;
        }
    }
}
