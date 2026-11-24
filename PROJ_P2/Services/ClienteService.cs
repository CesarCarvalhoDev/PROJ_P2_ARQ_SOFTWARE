using PROJ_P2.Models;
using PROJ_P2.Repositories;

namespace PROJ_P2.Services;

/// <summary>
/// O "cozinheiro": conhece as regras do negócio.
/// Não sabe nada de HTTP nem de Entity Framework.
/// </summary>
public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repositorio;

    public ClienteService(IClienteRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<Cliente>> ListarAsync()
    {
        return await _repositorio.ListarAsync();
    }

    // Devolve null quando o Cliente não existe
    public async Task<Cliente?> ObterAsync(int id)
    {
        return await _repositorio.ObterAsync(id);
    }

    public async Task<Cliente> CadastrarAsync(Cliente cliente)
    {
        cliente.Nome = cliente.Nome.Trim();
        cliente.Telefone = cliente.Telefone.Trim();

        await ValidarRegrasAsync(cliente, idIgnorado: null);

        cliente.Id = 0;
        _repositorio.Adicionar(cliente);
        await _repositorio.SalvarAsync();

        return cliente;
    }

    // Devolve null quando o Cliente não existe
    public async Task<Cliente?> AtualizarAsync(Cliente clienteAtualizado)
    {
        var cliente = await _repositorio.ObterAsync(clienteAtualizado.Id);

        if (cliente is null)
        {
            return null;
        }

        clienteAtualizado.Nome = clienteAtualizado.Nome.Trim();
        clienteAtualizado.Telefone = clienteAtualizado.Telefone.Trim();

        await ValidarRegrasAsync(clienteAtualizado, idIgnorado: clienteAtualizado.Id);

        cliente.Nome = clienteAtualizado.Nome;
        cliente.Telefone = clienteAtualizado.Telefone;

        await _repositorio.SalvarAsync();

        return cliente;
    }

    // Devolve false quando o Cliente não existe
    public async Task<bool> RemoverAsync(int id)
    {
        var cliente = await _repositorio.ObterAsync(id);

        if (cliente is null)
        {
            return false;
        }

        _repositorio.Remover(cliente);
        await _repositorio.SalvarAsync();

        return true;
    }

    // As regras de negócio moram aqui, num lugar só.
    // idIgnorado: na atualização, o próprio Cliente não conta como duplicado.
    private async Task ValidarRegrasAsync(Cliente cliente, int? idIgnorado)
    {
        // Regra 1: nome obrigatório
        if (string.IsNullOrWhiteSpace(cliente.Nome))
        {
            throw new RegraDeNegocioException("O nome do cliente é obrigatório.");
        }

        // Regra 2: telefone obrigatório
        if (string.IsNullOrWhiteSpace(cliente.Telefone))
        {
            throw new RegraDeNegocioException("O telefone do cliente é obrigatório.");
        }

        // Regra 3: não permitir telefone duplicado
        if (await _repositorio.ExisteAsync(cliente.Telefone, idIgnorado))
        {
            throw new RegraDeNegocioException("Já existe um cliente cadastrado com esse telefone.");
        }
    }
}