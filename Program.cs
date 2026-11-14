
// José Raúl Guerrero Parra

using Ejercicio2_APIDataSphere_Rg3.Models;
using Ejercicio2_APIDataSphere_Rg3.Repositories;
using Ejercicio2_APIDataSphere_Rg3.UnitOfWork;
using Ejercicio2_APIDataSphere_Rg3.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Ejercicio2_APIDataSphere_Rg3.Security;

var builder = WebApplication.CreateBuilder(args);


// 1. CONFIGURACION JWT - DEMOSTRACION LOCAL
// Clave utilizada exclusivamente para las pruebas locales.
// En produccion debe almacenarse fuera del codigo fuente.

// MODIFICACION:
// Leer la configuracion JWT desde User Secrets mediante JwtSettings.

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>()
    ?? throw new InvalidOperationException(
        "No se ha encontrado la configuracion JwtSettings.");

var jwtKey = jwtSettings.Key;
var jwtIssuer = jwtSettings.Issuer;
var jwtAudience = jwtSettings.Audience;

// Configuracion de autenticacion JWT.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            RoleClaimType = ClaimTypes.Role,

            ClockSkew = TimeSpan.Zero
        };
    });


// Politica de autorizacion por rol.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RiskAnalystOnly", policy =>
        policy.RequireAuthenticatedUser()
              .RequireRole("RiskAnalyst"));
});


// 2. REPOSITORY PATTERN Y UNIT OF WORK (SEMANA 2)
// Registro de dependencias.
builder.Services.AddSingleton<IAssetRepository, AssetRepository>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

// Configuracion Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// 3. CONFIGURACION DEL PIPELINE HTTP
// Swagger disponible en desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireccion HTTP a HTTPS.
app.UseHttpsRedirection();

// Activar autenticacion y autorizacion.
app.UseAuthentication();
app.UseAuthorization();



// 4. LOGIN DE DEMOSTRACION - GENERACION JWT (SEMANA 2)
// Credenciales utilizadas exclusivamente para pruebas locales.

app.MapPost("/auth/login", (LoginRequest login) =>
{
    string rol;

    if (login.Username == "analista" &&
        login.Password == "DemoAnalista2026!")
    {
        rol = "RiskAnalyst";
    }
    else if (login.Username == "usuario" &&
             login.Password == "DemoUsuario2026!")
    {
        rol = "User";
    }
    else
    {
        return Results.Unauthorized();
    }

    // Claims: identidad y rol del usuario.
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, login.Username),
        new Claim(ClaimTypes.Role, rol)
    };

    // Clave de firma del JWT.
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtKey));

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);

    // Crear token con una duracion de 1 hora.
    var token = new JwtSecurityToken(
        issuer: jwtIssuer,
        audience: jwtAudience,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    var jwt = new JwtSecurityTokenHandler()
        .WriteToken(token);

    return Results.Ok(new
    {
        Token = jwt,
        Role = rol
    });
});



// 5. ENDPOINTS CRUD DE ACTIVOS (SEMANA 2)
// GET: Obtener todos los activos.

app.MapGet("/assets", (IUnitOfWork unitOfWork) =>
{
    return Results.Ok(unitOfWork.Assets.GetAll());
});


// GET: Obtener un activo por ID.
app.MapGet("/assets/{id:int}", (int id, IUnitOfWork unitOfWork) =>
{
    var asset = unitOfWork.Assets.GetById(id);

    return asset is null
        ? Results.NotFound()
        : Results.Ok(asset);
});


// POST: Crear un activo.
app.MapPost("/assets", (Asset asset, IUnitOfWork unitOfWork) =>
{
    // Evitar identificadores duplicados.
    if (unitOfWork.Assets.GetById(asset.Id) != null)
    {
        return Results.Conflict(
            new { Message = "Ya existe un activo con ese ID" });
    }

    unitOfWork.Assets.Add(asset);

    return Results.Created($"/assets/{asset.Id}", asset);
});


// PUT: Actualizar un activo.
app.MapPut("/assets/{id:int}", (int id, Asset asset,
    IUnitOfWork unitOfWork) =>
{
    var existing = unitOfWork.Assets.GetById(id);

    if (existing is null)
    {
        return Results.NotFound();
    }

    asset.Id = id;
    unitOfWork.Assets.Update(asset);

    return Results.Ok(asset);
});


// DELETE: Eliminar un activo.
app.MapDelete("/assets/{id:int}", (int id, IUnitOfWork unitOfWork) =>
{
    var existing = unitOfWork.Assets.GetById(id);

    if (existing is null)
    {
        return Results.NotFound();
    }

    unitOfWork.Assets.Delete(id);

    return Results.NoContent();
});


// 6. ENDPOINT PROTEGIDO: EXPOSICION ECONOMICA (SEMANA 2)
// Este endpoint requiere autenticacion JWT y rol RiskAnalyst.

app.MapGet("/assets/total-exposure", (IUnitOfWork unitOfWork) =>
{
    var total = unitOfWork.Assets.GetTotalExposure();

    return Results.Ok(new
    {
        TotalExposure = total
    });
})
.RequireAuthorization("RiskAnalystOnly");


// 7. NUEVO ENDPOINT DE RIESGOS PARA REACT (añadido posterior)
//Endpoint de solo lectura.
//Devuelve los activos geolocalizados y sus exposiciones a diferentes hazards.
//Los datos son simulados y se almacenan en memoria mediante la clase RiskAssetsData.
//React consumira esta ruta mediante fetch() y mostrara los resultados en un listado y un mapa Leaflet.
//No hace falta autenticacion porque utilizamos informacion ficticia para la demostracion.

app.MapGet("/api/risk-assets", () =>
{
    //para obtener la coleccion de datos simulados:
    var riskAssets = RiskAssetsData.GetRiskAssets();

    //y devolver los registros en formato JSON con HTTP 200:
    return Results.Ok(riskAssets);
})
.WithName("GetRiskAssets")
.WithTags("DataSphere - Frontend")
.WithDescription(
    "Obtiene los assets simulados y sus exposiciones a hazards para el frontend React.");



// 8. EJECUTAR LA APLICACION

app.Run();



// MODELO PARA RECIBIR LAS CREDENCIALES DEL LOGIN

public record LoginRequest(string Username, string Password);
