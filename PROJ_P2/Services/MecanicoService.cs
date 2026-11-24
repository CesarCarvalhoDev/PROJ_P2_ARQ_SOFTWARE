using PROJ_P2.Models;

namespace PROJ_P2.Services;

/// <summary>
/// O "cozinheiro": conhece as regras do negócio.
/// Não sabe nada de HTTP nem de Entity Framework.
/// </summary>
public class MecanicoService : IMecanicoService
{
    private readonly IMecanicoRepository _repositorio;

    public MecanicoService(IMecanicoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<Mecanico>> ListarAsync()
    {
        return await _repositorio.ListarAsync();
    }

    // Devolve null quando o mecanico não existe
    public async Task<Mecanico?> ObterAsync(int id)
    {
        return await _repositorio.ObterAsync(id);
    }

    public async Task<Mecanico> CriarAsync(Mecanico mecanico)
    {
        await ValidarRegrasAsync(mecanico, idIgnorado: null);

        mecanico.Id = 0;
        _repositorio.Adicionar(mecanico);
        await _repositorio.SalvarAsync();

        return mecanico;
    }

    // Devolve false quando o mecanico não existe
    public async Task<bool> AtualizarAsync(int id, Mecanico mecanicoAtualizado)
    {
        var mecanico = await _repositorio.ObterAsync(id);

        if (mecanico is null)
        {
            return false;
        }

        await ValidarRegrasAsync(mecanicoAtualizado, idIgnorado: id);

        mecanico.Cpf = mecanicoAtualizado.Cpf;
        mecanico.Nome = mecanicoAtualizado.Nome;
        mecanico.Salario = mecanicoAtualizado.Salario;
        mecanico.NivelTecnico = mecanicoAtualizado.NivelTecnico;

        await _repositorio.SalvarAsync();

        return true;
    }

    // Devolve false quando o mecanico não existe
    public async Task<bool> RemoverAsync(int id)
    {
        var mecanico = await _repositorio.ObterAsync(id);

        if (mecanico is null)
        {
            return false;
        }

        _repositorio.Remover(mecanico);
        await _repositorio.SalvarAsync();

        return true;
    }

    // As regras de negócio moram aqui, num lugar só.
    // idIgnorado: no PUT, o próprio mecanico não conta como duplicado.
    private async Task ValidarRegrasAsync(Mecanico mecanico, int? idIgnorado)
    {
        // Regra 1: CPF é obrigatório
        if (string.IsNullOrWhiteSpace(mecanico.Cpf))
        {
            throw new RegraDeNegocioException("O CPF do mecanico é obrigatório.");
        }

        // Regra 2: não pode existir outro mecanico com o mesmo CPF
        if (await _repositorio.ExisteAsync(mecanico.Cpf, idIgnorado))
        {
            throw new RegraDeNegocioException("Já existe um mecanico cadastrado com esse CPF.");
        }

        // Regra 3: o nome é obrigatório
        if (string.IsNullOrWhiteSpace(mecanico.Nome))
        {
            throw new RegraDeNegocioException("O nome do mecanico é obrigatório.");
        }

        // Regra 4: o salário não pode ser negativo
        if (mecanico.Salario < 0)
        {
            throw new RegraDeNegocioException("O salário do mecanico não pode ser negativo.");
        }
    }
}