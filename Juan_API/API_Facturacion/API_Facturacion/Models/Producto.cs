using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models
{
    public class Producto
    {
        [Key]
        public Guid idProducto { get; set; }
        public int codigoBarra { get; set; }
        public string nombre { get; set; }
        public string description { get; set; }
        public decimal precioVenta { get; set; }
        public decimal precioCompra { get; set; }
        public decimal impuesto { get; set; }
        public string unidadMedida { get; set; }
        public string tipo { get; set; }
        public int existencia { get; set; }
    }
}
