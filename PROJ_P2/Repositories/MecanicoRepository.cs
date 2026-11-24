
using PROJ_P2.Data;
using PROJ_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace PROJ_P2.Services;

/// <summary>
/// O acesso aos dados dos mecanicos.
/// Não conhece regras de negócio, apenas consulta, adiciona, remove e grava.
/// </summary>
public class MecanicoRepository : IMecanicoRepository
{
    private readonly AppDbContext _context;

    public MecanicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Mecanico>> ListarAsync()
    {
        return await _context.Mecanicos.ToListAsync();
    }

    public async Task<Mecanico?> ObterAsync(int id)
    {
        return await _context.Mecanicos.FindAsync(id);
    }

    public async Task<bool> ExisteAsync(string cpf, int? idIgnorado)
    {
        return await _context.Mecanicos.AnyAsync(m => m.Cpf == cpf && m.Id != idIgnorado);
    }

    public void Adicionar(Mecanico mecanico)
    {
        _context.Mecanicos.Add(mecanico);
    }

    public void Remover(Mecanico mecanico)
    {
        _context.Mecanicos.Remove(mecanico);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}
