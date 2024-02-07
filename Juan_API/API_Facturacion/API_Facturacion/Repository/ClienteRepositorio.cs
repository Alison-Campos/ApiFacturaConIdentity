using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API_Facturacion.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly Context db;
        public ClienteRepositorio(Context _db)
        {
            db = _db;
        }
        public bool ActualizarCliente(Cliente client)
        {
            db.Clientes.Update(client);
            return Guardar();
        }

        public bool BorrarCliente(Cliente client)
        {
            db.Clientes.Remove(client);
            return Guardar();
        }

        public bool CrearCliente(Cliente client)
        {
            db.Clientes.Add(client);
            return Guardar();
        }

        public bool ExisteCliente(string cedula)
        {
            bool exist = db.Clientes.Any(c=> c.cedula == cedula);
            return exist;
        }

        public bool ExisteCliente(Guid id)
        {
            bool exists = db.Clientes.Any(c => c.IdCliente == id);
            return exists;
        }

        public Cliente GetCliente(Guid id)
        {
            Cliente cliente = db.Clientes.FirstOrDefault(c => c.IdCliente == id);
            return cliente;
        }

        public ICollection<Cliente> GetClientes()
        {
            return db.Clientes.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return db.SaveChanges() >= 0? true: false;
        }
    }
}
