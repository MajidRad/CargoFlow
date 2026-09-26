using CargoFlow.Identity.Application.DependencyInjection;
using CargoFlow.Identity.Infrastructure.DependencyInjection;
using CargoFlow.Identity.Infrastructure.Keycloak.Clients;
using CargoFlow.Identity.Infrastructure.Keycloak.Seeders;
using CargoFlow.Identity.Infrastructure.Persistence;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");
        options.Authority = jwt["Authority"];
        options.Audience = jwt["Audience"];
        options.RequireHttpsMetadata =
        bool.Parse(jwt["RequireHttpsMetadata"]!);

    });
builder.Services.AddAuthorization();
builder.Services.AddCarter();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    await dbContext.Database.MigrateAsync();
    var keycloakHealthChecker = scope.ServiceProvider.GetRequiredService<IKeycloakHealthChecker>();
    await keycloakHealthChecker.WaitUntilReadyAsync();
    var keycloakSeeder = scope.ServiceProvider.GetRequiredService<IKeycloakSeeder>();
    await keycloakSeeder.SeedAsync();
}
// Configure the HTTP request pipeline.
app.MapOpenApi();
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.Title = "CargoFlow API";
        options.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();

