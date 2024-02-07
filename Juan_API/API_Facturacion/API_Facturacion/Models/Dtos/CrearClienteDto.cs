using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models.Dtos
{
    public class CrearClienteDto
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(70,ErrorMessage ="el numero maximo de caracteres es de 70")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La Cedula es obligatorio")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "El Correo es obligatorio")]
        public string? email { get; set; }
    }
}
