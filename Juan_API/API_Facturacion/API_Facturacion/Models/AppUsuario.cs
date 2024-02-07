using Microsoft.AspNetCore.Identity;

namespace API_Facturacion.Models
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre {  get; set; }

    }
}
