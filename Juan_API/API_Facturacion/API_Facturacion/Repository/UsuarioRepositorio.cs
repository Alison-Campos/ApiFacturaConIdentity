using API_Facturacion.Data;
using API_Facturacion.Models;
using API_Facturacion.Models.Dtos;
using API_Facturacion.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace API_Facturacion.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Context db;
        private string claveSecreta;
        public UsuarioRepositorio(Context _db, IConfiguration configuration)
        {
            db = _db;
            claveSecreta = configuration.GetValue<string>("ApiSettings:Secreta");
        }
        public ICollection<Usuario> GetUsuarios()
        {
            return db.Usuarios.OrderBy(u => u.Nombre).ToList();
        }

        public Usuario GetUsuario(Guid id)
        {
            return db.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public bool IsUniqueUser(string email)
        {
            var usuarioId = db.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuarioId == null)
            {
                return true;
            }
            return false;
        }
        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passworEncriptado = obtenermd5(usuarioRegistroDto.Password);
            Usuario usuario = new Usuario()
            {
                Email = usuarioRegistroDto.Email,
                Password = passworEncriptado,
                Nombre = usuarioRegistroDto.Nombre,
                Role = usuarioRegistroDto.Role,
            };
            db.Add(usuario);
            await db.SaveChangesAsync();
            usuario.Password = passworEncriptado;
            return usuario;

        }
        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);

            // Utilizar FirstOrDefaultAsync para operaciones asincrónicas en la base de datos
            var usuario = await db.Usuarios.FirstOrDefaultAsync(
                u => u.Email.ToLower() == usuarioLoginDto.Email.ToLower()
                && u.Password == passwordEncriptado
            );

            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto
                {
                    Token = "",
                    Usuario = null
                };
            }
            var manejarToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString() ),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = manejarToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto
            {
                Token = manejarToken.WriteToken(token),
                Usuario = usuario
            };
            return usuarioLoginRespuestaDto;
        }

        //Método para encriptar contraseña con MD5 se usa tanto en el Acceso como en el Registro
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }

}
