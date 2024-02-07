using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;
using API_Facturacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaRepositorio ctRepo;
        private readonly IMapper mapper;
        public FacturasController(IFacturaRepositorio _ctRepo, IMapper _mapper)
        {
            ctRepo = _ctRepo;
            mapper = _mapper;           
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFacturas()
        {
            var lista = ctRepo.GetFacturas();
            var listaDto = new List<FacturaDto>();
            foreach (var item in lista)
            {
                listaDto.Add(mapper.Map<FacturaDto>(item));
            }
            return Ok(listaDto);
        }
        [HttpGet("{numeroFactura:int}", Name = "GetFactura")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetFactura(int numeroFactura)
        {
            var fact = ctRepo.GetFactura(numeroFactura);
            if (fact == null)
            {
                return NotFound();
            }
            var facturaDto = mapper.Map<FacturaDto>(fact);
            return Ok(facturaDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(FacturaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFactura(CrearFacturaDto facturaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (facturaDto == null)
            {
                return BadRequest(ModelState);
            }
            var factura = mapper.Map<Factura>(facturaDto);
            if (!ctRepo.CrearFactura(factura))
            {
                ModelState.AddModelError("", "Algo salio mal guardando el registro");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetFactura", new { numeroFactura = factura.numeroFactura }, factura);
        }
        [HttpPatch("{numeroFactura:int}", Name = "UpdateFactura")]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFactura(int numeroFactura, [FromBody] FacturaDto facturaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(facturaDto == null || facturaDto.numeroFactura != numeroFactura)
            {
                return BadRequest(ModelState);
            }
            var factura = mapper.Map<Factura>(facturaDto);
            if(!ctRepo.ActualizarFactura(factura))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{factura.numeroFactura}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{numeroFactura:int}", Name = "DeleteFactura")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFactura(int numeroFactura)
        {
            if (!ctRepo.ExisteFactura(numeroFactura))
            {
                return NotFound();
            }
            var factura = ctRepo.GetFactura(numeroFactura);
            if (!ctRepo.BorrarFactura(factura))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro{factura.numeroFactura}");
                return StatusCode(500, ModelState);
            }
            return NotFound();
        }
        

        }//Clase
}//Nombre 
