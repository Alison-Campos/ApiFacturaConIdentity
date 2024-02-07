using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;
using AutoMapper;

namespace API_Facturacion.AutoMapper
{
    public class AplicacionMappers: Profile
    {
        public AplicacionMappers()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, CrearProductoDto>().ReverseMap();
            CreateMap<Cliente, CrearClienteDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Factura, FacturaDto>().ReverseMap();
            CreateMap<Factura, CrearFacturaDto>().ReverseMap();
            CreateMap<FacturaDetalle, FacturaDetalleDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioRegistroDto>().ReverseMap();
            CreateMap<Usuario, UsuarioRegistroDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginRespuestaDto>().ReverseMap();
        }
    }
}
