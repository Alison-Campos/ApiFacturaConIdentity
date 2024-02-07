using API_Facturacion.Models;

namespace API_Facturacion.Repository.IRepository
{
    public interface IClienteRepositorio
    {
        ICollection<Cliente> GetClientes();
        Cliente GetCliente(Guid id);
        bool ExisteCliente(string cedula);
        bool ExisteCliente(Guid id);
        bool CrearCliente(Cliente client);
        bool ActualizarCliente(Cliente client);
        bool BorrarCliente(Cliente client);
        bool Guardar();
    }
}
