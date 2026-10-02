using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Persistence.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task<WebApplication> InitialDatabaseAsync(this WebApplication app)
    {
        await DatabaseInitializer.InitialiseAsync(app.Services);
        return app; 
    }
}
