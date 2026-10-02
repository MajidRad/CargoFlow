using CargoFlow.BuildingBlocks.Application.DependencyInjection;
using CargoFlow.Identity.Application.DependencyInjection;
using CargoFlow.Identity.Infrastructure.Authentication;
using CargoFlow.Identity.Infrastructure.DependencyInjection;
using CargoFlow.Identity.Infrastructure.Authorization;
using Carter;
using CargoFlow.Identity.Infrastructure.Persistence.Extensions;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddBuildingBlocksApplication();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var jwtOptions = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtOptions>()!;

builder.Services.AddJwtAuthentication(jwtOptions);
builder.Services.AddPermissionAuthorization();

builder.Services.AddCarter();
var app = builder.Build();
await app.InitialDatabaseAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Cargoflow.Identity";
        options.Theme = ScalarTheme.Purple;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();

