using TaskBoard.Service.Core.Api.Extensions;
using TaskBoard.Framework.Core.Middlewares;
using TaskBoard.Framework.Core.Security.Keycloak;
using TaskBoard.Framework.Core.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDependencies(builder.Configuration);

builder.Services.AddControllers();

builder.Services.ConfigurePersistence(builder.Configuration);

builder.Services.AddAuthenticationWithKeycloakConfiguration(builder.Configuration.GetSection("Keycloak"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ControllerAdviceMiddleware>();
app.UseMiddleware<AuthenticatedUserMiddleware>();
app.EnsureDatabaseCreated();

app.MapControllers();

await app.RunAsync();

public partial class Program {}
