using System.Text.Json.Serialization;

namespace PROJ_P2.Models;

public class Carro
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int ClienteId { get; set; }

    [JsonIgnore]
    public Cliente? Cliente { get; set; }
}