using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;

namespace API_Facturacion.Repository.IRepository
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(Guid id);
        bool IsUniqueUser(string email);
       Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
       Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);

    }
}
