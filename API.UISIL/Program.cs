using API.DAPPER;
using API.LOGICA.NEGOCIO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<InicioSesionBLL>();
builder.Services.AddScoped<IniciosSesionDAL>();
builder.Services.AddScoped<UsuariosBLL>();
builder.Services.AddScoped<UsuariosDAL>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{

    options.AddPolicy("PermitirTodos", policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("PermitirTodos");

app.UseAuthorization();

app.MapControllers();

app.Run();
