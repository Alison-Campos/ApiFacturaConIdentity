using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;
using API_Facturacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct.Security;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace API_Facturacion.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio ctRepo;
        private readonly IMapper mapper;
        public ProductosController(IProductoRepositorio _ctRepo, IMapper _mapper)
        {
            this.ctRepo = _ctRepo;
            this.mapper = _mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        //[ResponseCache(Duration =20)]
        [ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductos()
        {
            var lista = ctRepo.GetProductos();
            var listaProductosDto = new List<ProductoDto>();

            foreach (var item in lista) 
            { 
                listaProductosDto.Add(mapper.Map<ProductoDto>(item));
            }
            return Ok(listaProductosDto);
        }
        [AllowAnonymous]
        [HttpGet("{productoId:Guid}",Name ="GetProducto")]
        [ResponseCache(CacheProfileName = "PorDefecto20Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProducto(Guid productoId)
        {
            var itemProducto = ctRepo.GetProducto(productoId);
            if(itemProducto == null)
            {
                return NotFound();
            }
            var productoDto = mapper.Map<ProductoDto>(itemProducto);
            return Ok(productoDto);
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        [ProducesResponseType(201, Type= typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       

        public IActionResult CreateProducto([FromBody] CrearProductoDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (product == null)
            {
                return BadRequest(ModelState);
            }
            if(ctRepo.ExisteProducto(product.nombre))
            {
                ModelState.AddModelError("", "el producto ya existe");
                return StatusCode(404, ModelState);
            }
            var producto = mapper.Map<Producto>(product);
            if (!ctRepo.CrearProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{producto.nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetProducto", new { productoId = producto.idProducto }, producto);
        }
        [Authorize(Roles = "admin")]

        [HttpPatch("{productoId:guid}", Name = "UpdateProducto")]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public IActionResult UpdateProducto(Guid productoId,[FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productoDto == null || productoDto.idProducto!= productoId)
            {
                return BadRequest(ModelState);
            }
            var producto = mapper.Map<Producto>(productoDto);
            if (!ctRepo.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{producto.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{productoId:guid}", Name = "DeleteProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [Authorize(Roles = "admin")]

        public IActionResult DeleteProducto(Guid productoId)
        {
            
            if(!ctRepo.ExisteProducto(productoId))
            {  
                return NotFound();
            }
            var producto = ctRepo.GetProducto(productoId);
            if (!ctRepo.BorrarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro{producto.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }//finClase
}//FinNamespace
