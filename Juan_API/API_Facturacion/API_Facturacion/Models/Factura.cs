using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models
{
    public class Factura
    {
        [Key]
        public int numeroFactura {  get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public Guid idCliente { get; set; }
        [Required]
        public decimal subTotal { get; set; }
        public decimal montoDescuento { get; set; }
        public decimal montoImpuesto { get; set; }
        [Required]
        public decimal total { get; set; }
        public Guid? usuario { get; set; }
        public string tipoPago { get; set;}

    }
}
