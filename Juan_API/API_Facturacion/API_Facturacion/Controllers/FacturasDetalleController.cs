using API_Facturacion.Models.Dtos;
using API_Facturacion.Models;
using API_Facturacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasDetalleController : ControllerBase
    {
        private readonly IFacturaDetRepositorio ctRepo;
        private readonly IMapper mapper;
        public FacturasDetalleController(IFacturaDetRepositorio _ctRepo, IMapper _mapper)
        {
            ctRepo = _ctRepo;
            mapper = _mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFacturasDetalle()
        {
            var lista = ctRepo.GetFactursDetalle();
            var listaDto = new List<FacturaDetalleDto>();
            foreach (var item in lista)
            {
                listaDto.Add(mapper.Map<FacturaDetalleDto>(item));
            }
            return Ok(listaDto);
        }
        [HttpGet("{IdDetalle:Guid}", Name = "GetFacturaDetalle")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetFacturaDetalle(Guid IdDetalle)
        {
            var factDetalle = ctRepo.GetFacturaDetalle(IdDetalle);
            if (factDetalle == null)
            {
                return NotFound();
            }
            var facturaDto = mapper.Map<FacturaDetalleDto>(factDetalle);
            return Ok(facturaDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(FacturaDetalleDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFacturaDetalle(FacturaDetalleDto facturaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (facturaDto == null)
            {
                return BadRequest(ModelState);
            }

            FacturaDetalle factura = new FacturaDetalle();
            factura = mapper.Map<FacturaDetalle>(facturaDto);
            factura.idProducto = (Guid)facturaDto.idProducto;
            if (!ctRepo.CrearFacturaDetalle(factura))
            {
                ModelState.AddModelError("", "Algo salio mal guardando el registro");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetFacturaDetalle", new { IdDetalle = factura.IdDetalle }, factura);
        }
        [HttpPatch("{IdDetalle:Guid}", Name = "UpdateFacturaDetalle")]
        [ProducesResponseType(201, Type = typeof(FacturaDetalleDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFacturaDetalle(Guid IdDetalle, [FromBody] FacturaDetalleDto facturaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (facturaDto == null || facturaDto.IdDetalle != IdDetalle)
            {
                return BadRequest(ModelState);
            }
            var factura = mapper.Map<FacturaDetalle>(facturaDto);
            if (!ctRepo.ActualizarFacturaDetalle(factura))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{factura.numeroFactura}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{IdDetalle:Guid}", Name = "DeleteFacturaDetalle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFacturaDetalle(Guid IdDetalle)
        {
            if (!ctRepo.ExisteFacturaDetalle(IdDetalle))
            {
                return NotFound();
            }
            var factura = ctRepo.GetFacturaDetalle(IdDetalle);
            if (!ctRepo.BorrarFacturaDetalle(factura))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro{factura.numeroFactura}");
                return StatusCode(500, ModelState);
            }
            return NotFound();
        }
    }
}
