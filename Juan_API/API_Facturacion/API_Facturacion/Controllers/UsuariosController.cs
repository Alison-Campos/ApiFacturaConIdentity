using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;
using API_Facturacion.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_Facturacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio usRepo;
        private readonly IMapper mapper;
        protected RespuestaAPI _respuestaApi;
        public UsuariosController(IUsuarioRepositorio _usRepo, IMapper _mapper)
        {
            usRepo = _usRepo;
            mapper = _mapper;
            this._respuestaApi = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var lista = usRepo.GetUsuarios();
            var listaDto = new List<UsuarioDto>();
            foreach (var item in lista)
            {
                listaDto.Add(mapper.Map<UsuarioDto>(item));
            }
            return Ok(listaDto);
        }
        [HttpGet("{IdUsuario:Guid}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsuario(Guid IdUsuario)
        {
            var usuario = usRepo.GetUsuario(IdUsuario);
            if (usuario == null)
            {
                return NotFound();
            }
            UsuarioDto usuarioDto = mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }

        

        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UsuarioRegistroDto usuarioDto)
        {
            bool vakidarUsuarioUnico = usRepo.IsUniqueUser(usuarioDto.Email);
            if (!vakidarUsuarioUnico)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El Email del Usuario ya existe anteriormente Registrado");
                return BadRequest(_respuestaApi);
            }
            var usuario = await usRepo.Registro(usuarioDto);
            if (usuario == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestaApi);
            }
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioDto)
        {
            var respuestaLogin = await usRepo.Login(usuarioDto);

            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El email o contraseña son incorrectos");
                return BadRequest(_respuestaApi);
            }
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            _respuestaApi.Result = respuestaLogin;
            return Ok(_respuestaApi);
        }
    }// cierre classs
}//cierre namesPase 
