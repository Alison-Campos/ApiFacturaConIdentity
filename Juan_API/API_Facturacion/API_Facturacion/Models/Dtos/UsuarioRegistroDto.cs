using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models.Dtos
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]

        public string Password { get; set; }
        public string Role { get; set; }
    }
}
