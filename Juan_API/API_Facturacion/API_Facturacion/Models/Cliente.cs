using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models
{
    public class Cliente
    {

        [Key]
        public Guid IdCliente { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string cedula { get; set; }
        public string? email { get; set; }
    }
}
