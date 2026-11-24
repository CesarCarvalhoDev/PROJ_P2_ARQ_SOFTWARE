using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public interface IOrdemServicoRepository
{
    Task<List<OrdemServico>> ListarAsync();
    Task<OrdemServico?> ObterAsync(int id);
    Task<bool> ExisteAsync(int numeroOrdem, int? idIgnorado);
    void Adicionar(OrdemServico ordemServico);
    void Remover(OrdemServico ordemServico);
    Task SalvarAsync();
}