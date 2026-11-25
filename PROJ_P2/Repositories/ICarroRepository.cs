using PROJ_P2.Models;

namespace PROJ_P2.Repositories;

public interface ICarroRepository
{
    Task<List<Carro>> ListarAsync();
    Task<Carro?> ObterAsync(int id);
    Task<bool> ExisteAsync(string placa, int? idIgnorado = null);
    void Adicionar(Carro carro);
    void Remover(Carro carro);
    Task SalvarAsync();
}