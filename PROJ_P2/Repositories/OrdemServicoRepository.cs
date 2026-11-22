using Microsoft.EntityFrameworkCore;
using PROJ_P2.Data;
using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public class OrdemServicoRepository : IOrdemServicoRepository
{
    private readonly AppDbContext _context;

    public OrdemServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrdemServico>> ListarAsync()
    {
        return await _context.OrdensServico
            .AsNoTracking()
            .Include(o => o.Mecanico)
            .Include(o => o.Carro)
            .ToListAsync();
    }

    public async Task<OrdemServico?> ObterAsync(int id)
    {
        return await _context.OrdensServico
            .Include(o => o.Mecanico)
            .Include(o => o.Carro)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> ExisteAsync(int numeroOrdem, int? idIgnorado)
    {
        return await _context.OrdensServico
            .AnyAsync(o => o.NumeroOrdem == numeroOrdem && o.Id != idIgnorado);
    }

    public void Adicionar(OrdemServico ordemServico)
    {
        _context.OrdensServico.Add(ordemServico);
    }

    public void Remover(OrdemServico ordemServico)
    {
        _context.OrdensServico.Remove(ordemServico);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}