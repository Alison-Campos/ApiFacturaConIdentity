using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Facturacion.Repository
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly Context db;
        public FacturaRepositorio(Context _db)
        {
            db = _db;
        }
        public bool ActualizarFactura(Factura fact)
        {
            db.Facturas.Update(fact);
            return Guardar();
        }

        public bool BorrarFactura(Factura fact)
        {
            db.Facturas.Remove(fact);
            return Guardar();
        }

        public bool CrearFactura(Factura fact)
        {
            db.Facturas.Add(fact);
            return Guardar();
        }

        public bool ExisteFactura(string nombre)
        {
            bool valor = false;
            return valor;
        }

        public bool ExisteFactura(int id)
        {
            bool valor = db.Facturas.Any(c => c.numeroFactura == id);
            return valor;
        }

        public ICollection<Factura> GetFacturas()
        {
            var lista= db.Facturas.OrderBy(f=>f.numeroFactura).ToList();
            return lista;
        }

        public Factura GetFactura(int id)
        {
            Factura factura = db.Facturas.FirstOrDefault(f => f.numeroFactura == id);
            return factura;
        }

        public bool Guardar()
        {
            return db.SaveChanges () >= 0 ? true : false;
        }
    }
}
