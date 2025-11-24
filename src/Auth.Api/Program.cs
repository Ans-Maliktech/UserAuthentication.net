using Auth.Api.Configuration;
using Auth.Api.EndpointBuilders;
using Auth.Api.Midllewares;
using Auth.Core.Configuration;
using Auth.Infrastructure.Configuration;
using SmallApiToolkit.Extensions;
using SmallApiToolkit.Middleware;
using Auth.Infrastructure.Database.EFContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Load Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

// 2. Register Layers
builder.Services
    .AddApi()
    .AddCore()
    .AddInfrastructure(builder.Configuration);

// 3. *** DATABASE INTEGRATION ***
// We explicitly register SQL Server here. 
// Note: If 'AddInfrastructure' also registers a DbContext, this line should act as the definitive configuration.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(connectionString));

var corsPolicyName = builder.Services.AddCorsByConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ClaimsMiddleware>();

app.BuildAuthEndpoints();
app.BuildUserEndpoints();
app.BuildServiceEndpoints();

// 4. *** MIGRATION & SEEDING ***
// This applies the SQL Schema automatically on startup.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<UserContext>();
        // This creates the DB if it doesn't exist and applies all pending migrations
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// await app.Services.AddDefaultRoles();
// await app.Services.AddDefaultUsers();

app.Run();