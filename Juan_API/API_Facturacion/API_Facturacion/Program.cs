using API_Facturacion.AutoMapper;
using API_Facturacion.Data;
using API_Facturacion.Repository;
using API_Facturacion.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConectionSQL"));
});

//Soporte para autenticacion con .Net Entity
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Context>();

//anadimos cache
builder.Services.AddResponseCaching();

//Agregamos los repositorios
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();
builder.Services.AddScoped<IFacturaDetRepositorio, FacturaDetRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Agregamos los automappers
builder.Services.AddAutoMapper(typeof(AplicacionMappers));

// autentucacion
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Add services to the container.

builder.Services.AddControllers(opcion=>
{
    opcion.CacheProfiles.Add("PorDefecto20Segundos", new CacheProfile() { Duration = 30});
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description=
        "Autenticacion JWT usando el esquema Bearer. \r\n\r\n "+
        "Ingresa la palabra 'Beare' seguda de un [espacio] y despues su token en el campo de abajo \r\n\r\n "+
        "Ejemplo: \"bearer tdffsgdgdgff\"",
        Name= "Authorization",
        In= ParameterLocation.Header,
        Scheme= "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id= "Bearer"
                },
                Scheme= "oauth2",
                Name= "Bearer",
                In= ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//soporte Cors
builder.Services.AddCors(p => p.AddPolicy("PolicyCors",build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//soporte Cors
app.UseCors("PolicyCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

var culture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
