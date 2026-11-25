using PROJ_P2.Models;
using PROJ_P2.Repositories;

namespace PROJ_P2.Services;

/// <summary>
/// O "cozinheiro": conhece as regras do negócio.
/// Não sabe nada de HTTP nem de Entity Framework.
/// </summary>
public class CarroService : ICarroService
{
    private readonly ICarroRepository _repositorio;

    public CarroService(ICarroRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<Carro>> ListarAsync()
    {
        return await _repositorio.ListarAsync();
    }

    // Devolve null quando o Carro não existe
    public async Task<Carro?> ObterAsync(int id)
    {
        return await _repositorio.ObterAsync(id);
    }

    public async Task<Carro> CadastrarAsync(Carro carro)
    {
        NormalizarCampos(carro);

        await ValidarRegrasAsync(carro, idIgnorado: null);

        carro.Id = 0;
        carro.Cliente = null;
        _repositorio.Adicionar(carro);
        await _repositorio.SalvarAsync();

        return carro;
    }

    // Devolve null quando o Carro não existe
    public async Task<Carro?> AtualizarAsync(Carro carroAtualizado)
    {
        var carro = await _repositorio.ObterAsync(carroAtualizado.Id);

        if (carro is null)
        {
            return null;
        }

        NormalizarCampos(carroAtualizado);

        await ValidarRegrasAsync(carroAtualizado, idIgnorado: carroAtualizado.Id);

        carro.Placa = carroAtualizado.Placa;
        carro.Marca = carroAtualizado.Marca;
        carro.Modelo = carroAtualizado.Modelo;
        carro.Ano = carroAtualizado.Ano;
        carro.Tipo = carroAtualizado.Tipo;
        carro.ClienteId = carroAtualizado.ClienteId;

        await _repositorio.SalvarAsync();

        return carro;
    }

    // Devolve false quando o Carro não existe
    public async Task<bool> RemoverAsync(int id)
    {
        var carro = await _repositorio.ObterAsync(id);

        if (carro is null)
        {
            return false;
        }

        _repositorio.Remover(carro);
        await _repositorio.SalvarAsync();

        return true;
    }

    private static void NormalizarCampos(Carro carro)
    {
        carro.Placa = carro.Placa.Trim().ToUpperInvariant();
        carro.Marca = carro.Marca.Trim();
        carro.Modelo = carro.Modelo.Trim();
        carro.Tipo = carro.Tipo.Trim();
    }

    // As regras de negócio moram aqui, num lugar só.
    // idIgnorado: na atualização, o próprio Carro não conta como duplicado.
    private async Task ValidarRegrasAsync(Carro carro, int? idIgnorado)
    {
        // Regra 1: placa obrigatória
        if (string.IsNullOrWhiteSpace(carro.Placa))
        {
            throw new RegraDeNegocioException("A placa do carro é obrigatória.");
        }

        // Regra 2: não permitir placa duplicada
        if (await _repositorio.ExisteAsync(carro.Placa, idIgnorado))
        {
            throw new RegraDeNegocioException("Já existe um carro cadastrado com essa placa.");
        }

        // Regra 3: marca, modelo e tipo obrigatórios
        if (string.IsNullOrWhiteSpace(carro.Marca) ||
            string.IsNullOrWhiteSpace(carro.Modelo) ||
            string.IsNullOrWhiteSpace(carro.Tipo))
        {
            throw new RegraDeNegocioException("Marca, modelo e tipo do carro são obrigatórios.");
        }

        // Regra 4: ano válido
        var anoMaximo = DateTime.Now.Year + 1;
        if (carro.Ano < 1886 || carro.Ano > anoMaximo)
        {
            throw new RegraDeNegocioException($"O ano do carro deve estar entre 1886 e {anoMaximo}.");
        }

        // Regra 5: cliente obrigatório
        if (carro.ClienteId <= 0)
        {
            throw new RegraDeNegocioException("É obrigatório informar um cliente válido.");
        }
    }
}