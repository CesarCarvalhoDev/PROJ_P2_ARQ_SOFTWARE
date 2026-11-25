using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public interface IClienteRepository
{
    Task<List<Cliente>> ListarAsync();
    Task<Cliente?> ObterAsync(int id);
    Task<bool> ExisteAsync(string telefone, int? idIgnorado = null);
    void Adicionar(Cliente cliente);
    void Remover(Cliente cliente);
    Task SalvarAsync();
}