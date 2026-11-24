using Microsoft.EntityFrameworkCore;
using PROJ_P2.Data;
using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _contexto;

    public ClienteRepository(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Cliente>> ListarAsync()
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> ObterAsync(int id)
    {
        return await _contexto.Clientes
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ExisteAsync(string telefone, int? idIgnorado = null)
    {
        return await _contexto.Clientes
            .AnyAsync(c => c.Telefone == telefone && (idIgnorado == null || c.Id != idIgnorado));
    }

    public void Adicionar(Cliente cliente)
    {
        _contexto.Clientes.Add(cliente);
    }

    public void Remover(Cliente cliente)
    {
        _contexto.Clientes.Remove(cliente);
    }

    public async Task SalvarAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}