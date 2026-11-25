using PROJ_P2.Models;

namespace PROJ_P2.Services;

/// <summary>
/// O "cardápio": o que o serviço de mecanicos sabe fazer.
/// </summary>
public interface IMecanicoService
{
    Task<List<Mecanico>> ListarAsync();
    Task<Mecanico?> ObterAsync(int id);
    Task<Mecanico> CriarAsync(Mecanico mecanico);
    Task<bool> AtualizarAsync(int id, Mecanico mecanicoAtualizado);
    Task<bool> RemoverAsync(int id);
} 