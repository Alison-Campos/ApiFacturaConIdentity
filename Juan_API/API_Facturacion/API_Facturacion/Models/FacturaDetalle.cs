using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Facturacion.Models
{
    public class FacturaDetalle
    {
        [Key]

        public Guid IdDetalle { get; set; }
        
        public int cantidad { get; set; }
        public decimal? precioUnitario { get; set; }
        public decimal? subTotal { get; set; }
        public decimal? porImp { get; set; }
        public decimal? porDescuento { get; set; }
        [ForeignKey("numeroFactura")]
        public int numeroFactura { get; set; }
        [ForeignKey("idProducto")]
        public Guid idProducto { get; set; }
        public Producto Producto { get; set; }

    }
}
