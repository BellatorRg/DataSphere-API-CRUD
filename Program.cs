
using Ejercicio2_APIDataSphere_Rg3.Models;
using Ejercicio2_APIDataSphere_Rg3.Repositories;
using Ejercicio2_APIDataSphere_Rg3.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Configuramos el repositorio y Unit of Work
builder.Services.AddSingleton<IAssetRepository, AssetRepository>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET: Obtener todos los activos
app.MapGet("/assets", (IUnitOfWork unitOfWork) =>
{
    return Results.Ok(unitOfWork.Assets.GetAll());
});

// GET: Obtener un activo por ID
app.MapGet("/assets/{id:int}", (int id, IUnitOfWork unitOfWork) =>
{
    var asset = unitOfWork.Assets.GetById(id);

    return asset is null
        ? Results.NotFound()
        : Results.Ok(asset);
});

// POST: Crear un activo
app.MapPost("/assets", (Asset asset, IUnitOfWork unitOfWork) =>
{
    unitOfWork.Assets.Add(asset);

    return Results.Created($"/assets/{asset.Id}", asset);
});

// PUT: Actualizar un activo
app.MapPut("/assets/{id:int}", (int id, Asset asset,
    IUnitOfWork unitOfWork) =>
{
    var existing = unitOfWork.Assets.GetById(id);

    if (existing is null)
        return Results.NotFound();

    asset.Id = id;
    unitOfWork.Assets.Update(asset);

    return Results.Ok(asset);
});

// DELETE: Eliminar un activo
app.MapDelete("/assets/{id:int}", (int id, IUnitOfWork unitOfWork) =>
{
    var existing = unitOfWork.Assets.GetById(id);

    if (existing is null)
        return Results.NotFound();

    unitOfWork.Assets.Delete(id);

    return Results.NoContent();
});

// GET: Calcular el valor total de los activos
app.MapGet("/assets/total-exposure", (IUnitOfWork unitOfWork) =>
{
    var total = unitOfWork.Assets.GetTotalExposure();

    return Results.Ok(new { TotalExposure = total });
});

app.Run();
