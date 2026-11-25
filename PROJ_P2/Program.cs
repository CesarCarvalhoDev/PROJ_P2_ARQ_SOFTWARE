using Microsoft.EntityFrameworkCore;
using PROJ_P2.Data;
using PROJ_P2.Repositories;
using PROJ_P2.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de Dados (AppDbContext)
// Para usar SQL Server em vez de SQLite, troca UseSqlite por UseSqlServer
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependências (Repositories e Services)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<ICarroRepository, CarroRepository>();
builder.Services.AddScoped<ICarroService, CarroService>();

builder.Services.AddScoped<IMecanicoRepository, MecanicoRepository>();
builder.Services.AddScoped<IMecanicoService, MecanicoService>();

builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();