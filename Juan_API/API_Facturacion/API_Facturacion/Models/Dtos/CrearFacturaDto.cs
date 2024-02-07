using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models.Dtos
{
    public class CrearFacturaDto
    {
        [Required(ErrorMessage = "la fecha es obligatoria")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "El idCliente es obligatorio")]
        public Guid idCliente { get; set; }
        public decimal subTotal { get; set; }
        public decimal montoDescuento { get; set; }
        public decimal montoImpuesto { get; set; }
        [Required(ErrorMessage = "El total es obligatorio")]
        public decimal total { get; set; }
        //[Required(ErrorMessage = "El usuario es obligatorio")]
        public Guid? usuario { get; set; }
        [Required(ErrorMessage = "El tipoPago es obligatorio")]
        public string tipoPago { get; set; }
    }
}
