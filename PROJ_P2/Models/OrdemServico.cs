
namespace PROJ_P2.Models;

public class OrdemServico
{
    public int Id { get; set; }
    public int NumeroOrdem { get; set; }
    public decimal Valor { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime? Termino { get; set; }

    // Chave Estrangeira e Navegação para Mecânico (FK_mecanico)
    public int MecanicoId { get; set; }
    public Mecanico Mecanico { get; set; } = null!;

    // Chave Estrangeira e Navegação para Carro (FK_carro)
    public int CarroId { get; set; }
    public Carro Carro { get; set; } = null!;
}