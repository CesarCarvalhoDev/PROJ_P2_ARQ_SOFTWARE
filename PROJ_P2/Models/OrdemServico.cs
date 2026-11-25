using System.Text.Json.Serialization;

namespace PROJ_P2.Models;

public class OrdemServico
{
    public int Id { get; set; }
    public int NumeroOrdem { get; set; }
    public decimal Valor { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime? Termino { get; set; }

    public int MecanicoId { get; set; }

    [JsonIgnore]
    public Mecanico? Mecanico { get; set; }

    public int CarroId { get; set; }

    [JsonIgnore]
    public Carro? Carro { get; set; }
}