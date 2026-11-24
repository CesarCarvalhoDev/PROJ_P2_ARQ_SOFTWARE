using System.ComponentModel.DataAnnotations;

namespace PROJ_P2.Models;

/// <summary>
/// Representa o mecânico.
/// </summary>
public class Mecanico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "CPF é obrigatório")]
    public string Cpf { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public decimal Salario { get; set; } = 0;

    public string NivelTecnico { get; set; } = string.Empty;
}