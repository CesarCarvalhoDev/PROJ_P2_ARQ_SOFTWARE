using PROJ_P2.Models;

namespace PROJ_P2.Services;

public interface IOrdemServicoService
{
    Task<List<OrdemServico>> ListarAsync();
    Task<OrdemServico?> ObterAsync(int id);
    Task<OrdemServico> AbrirAsync(OrdemServico ordemServico);
    Task<OrdemServico?> FecharOrdemAsync(OrdemServico ordemServico);
    Task<bool> RemoverAsync(int id);
}