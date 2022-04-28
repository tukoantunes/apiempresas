using ApiEmpresas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

DependencyInjection.Register(builder);
CorsConfiguration.Register(builder);
JWTConfiguration.Register(builder);
SwaggerConfiguration.Register(builder);

var app = builder.Build();

SwaggerConfiguration.Use(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

CorsConfiguration.Use(app);

app.Run();



