
using PROJ_P2.Models;

namespace PROJ_P2.Services;

/// <summary>
/// O que o serviço precisa saber sobre os mecanicos guardados.
/// Fica junto do serviço: quem define o contrato é quem precisa dele.
/// Repare: nada aqui fala de banco, SQL ou Entity Framework.
/// </summary>
public interface IMecanicoRepository
{
    Task<List<Mecanico>> ListarAsync();
    Task<Mecanico?> ObterAsync(int id);

    // "Existe um mecanico com este CPF?" (regra 1)
    Task<bool> ExisteAsync(string cpf, int? idIgnorado);

    void Adicionar(Mecanico mecanico);
    void Remover(Mecanico mecanico);

    // Grava de uma vez tudo o que foi adicionado, alterado ou removido
    Task SalvarAsync();
}
