using CargoFlow.Identity.Application.DependencyInjection;
using CargoFlow.Identity.Infrastructure.DependencyInjection;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

