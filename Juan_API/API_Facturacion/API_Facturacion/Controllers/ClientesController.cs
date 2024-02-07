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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio ctRepo;
        private readonly IMapper mapper;
        public ClientesController(IClienteRepositorio _ctRepo, IMapper _mapper)
        {
            ctRepo = _ctRepo;
            mapper = _mapper;   
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetClientes()
        {
            var lista = ctRepo.GetClientes();
            var listaDto = new List<ClienteDto>();
            foreach (var item in lista)
            {
               listaDto.Add(mapper.Map<ClienteDto>(item));
            }
            return Ok(listaDto);
        }
        [HttpGet("{clienteId:Guid}", Name = "GetCliente")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCliente(Guid clienteId)
        {
            var cliente = ctRepo.GetCliente(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            ClienteDto clienteDto = mapper.Map<ClienteDto>(cliente);
            return Ok(clienteDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateClient([FromBody] CrearClienteDto clienteDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (clienteDto == null)
            {
                return BadRequest(ModelState);
            }
            if(ctRepo.ExisteCliente(clienteDto.cedula))
            {
                ModelState.AddModelError("", "Un cliente con esa cedula, ya existe anteriormete registrado");
                return StatusCode(404, ModelState);
            }
            var cliente = mapper.Map<Cliente>(clienteDto);
            if (!ctRepo.CrearCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCliente", new { clienteId = cliente.IdCliente }, cliente);
        }

        [HttpPatch("{clienteId:guid}", Name = "UpdateCliente")]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public IActionResult UpdateCliente(Guid clienteId, [FromBody] ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (clienteDto == null || clienteDto.IdCliente != clienteId)
            {
                return BadRequest(ModelState);
            }
            var cliente = mapper.Map<Cliente>(clienteDto);
            if (!ctRepo.ActualizarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{clienteId:guid}", Name = "DeleteCliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public IActionResult DeleteCliente(Guid clienteId)
        {

            if (!ctRepo.ExisteCliente(clienteId))
            {
                return NotFound();
            }
            var cliente = ctRepo.GetCliente(clienteId);
            if (!ctRepo.BorrarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro{cliente.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }//clase
}//Namesspace
