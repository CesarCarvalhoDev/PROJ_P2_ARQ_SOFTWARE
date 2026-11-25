using Microsoft.EntityFrameworkCore;
using PROJ_P2.Data;
using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public class CarroRepository : ICarroRepository
{
    private readonly AppDbContext _contexto;

    public CarroRepository(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Carro>> ListarAsync()
    {
        return await _contexto.Carros
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Carro?> ObterAsync(int id)
    {
        return await _contexto.Carros
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ExisteAsync(string placa, int? idIgnorado = null)
    {
        return await _contexto.Carros
            .AnyAsync(c => c.Placa == placa && (idIgnorado == null || c.Id != idIgnorado));
    }

    public void Adicionar(Carro carro)
    {
        _contexto.Carros.Add(carro);
    }

    public void Remover(Carro carro)
    {
        _contexto.Carros.Remove(carro);
    }

    public async Task SalvarAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}