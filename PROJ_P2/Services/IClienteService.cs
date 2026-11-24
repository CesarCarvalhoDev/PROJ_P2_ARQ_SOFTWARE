using PROJ_P2.Models;

namespace PROJ_P2.Services;

public interface IClienteService
{
    Task<List<Cliente>> ListarAsync();
    Task<Cliente?> ObterAsync(int id);
    Task<Cliente> CadastrarAsync(Cliente cliente);
    Task<Cliente?> AtualizarAsync(Cliente cliente);
    Task<bool> RemoverAsync(int id);
}