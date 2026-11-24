using Microsoft.EntityFrameworkCore;
using PROJ_P2.Data;
using PROJ_P2.Repositories;
using PROJ_P2.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers e OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Banco de Dados (AppDbContext)
// Se usar outro banco, troque UseSqlServer por UseNpgsql (Postgres), UseSqlite ou UseMySql
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência (Repositories e Services)
builder.Services.AddScoped<IMecanicoRepository, MecanicoRepository>();
builder.Services.AddScoped<IMecanicoService, MecanicoService>();

builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();