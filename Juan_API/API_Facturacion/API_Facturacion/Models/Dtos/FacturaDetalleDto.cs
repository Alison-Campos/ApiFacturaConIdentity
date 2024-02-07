using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models.Dtos
{
    public class FacturaDetalleDto
    {
        [Key]
        public Guid IdDetalle { get; set; }
        [Required(ErrorMessage ="La Cantidad es obligatorio")]
        public int cantidad { get; set; }
        [Required(ErrorMessage = "El precioUnitario es obligatorio")]
        public decimal? precioUnitario { get; set; }
        public decimal? subTotal { get; set; }
        public decimal? porImp { get; set; }
        public decimal? porDescuento { get; set; }
        [Required(ErrorMessage = "El numeroFactura es obligatorio")]
        [ForeignKey("numeroFactura")]
        public int numeroFactura { get; set; }
        [Required(ErrorMessage = "El idProducto es obligatorio")]
        public Guid idProducto { get; set; }
    }
}
