using System.ComponentModel.DataAnnotations;

namespace API_Facturacion.Models.Dtos
{
    public class CrearProductoDto
    {
        [Required(ErrorMessage = "El Codigo de barra es obligatorio")]
        public int codigoBarra { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(70, ErrorMessage = "El Maximo de caracteres es de 70!")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El descripcion es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Maximo de caracteres es de 100!")]
        public string description { get; set; }
        public decimal precioVenta { get; set; }
        public decimal precioCompra { get; set; }
        public decimal impuesto { get; set; }
        public string unidadMedida { get; set; }
        public string tipo { get; set; }
        public int existencia { get; set; }
    }
}
