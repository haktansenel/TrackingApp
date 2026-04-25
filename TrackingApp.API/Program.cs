using Scalar.AspNetCore;
using TrackingApp.API.Configuration;
using TrackingApp.Core.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Variables
var connectionString = builder.Configuration.GetConnectionString("MssqlConnection");

#endregion



builder.Services.AddControllers();

builder.Services.AddOpenApi();

#region Custom DI Configurations

builder.Services.AddOptionsPattern(builder.Configuration);

builder.Services.AddAutoMapperProfiles();

builder.Services.AddDIMethods();

builder.Services.EFCoreConfiguration(connectionString);

builder.Services.AddJwtTokenConfiguration(builder.Configuration.GetSection("JwtSettings").Get<JwtSettingsOptions>());

builder.Services.AddHttpContextAccessor();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/test", () => "Çalışıyor").WithName("TestEndpoint");

app.MapControllers();

app.Run();
