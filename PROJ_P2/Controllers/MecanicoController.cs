using PROJ_P2.Models;
using PROJ_P2.Services;
using Microsoft.AspNetCore.Mvc;

namespace PROJ_P2.Controllers;

/// <summary>
/// O "garçom": recebe a requisição, chama o serviço e escolhe a resposta HTTP.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MecanicosController : ControllerBase
{
    private readonly IMecanicoService _servico;

    public MecanicosController(IMecanicoService servico)
    {
        _servico = servico;
    }

    // GET: api/Mecanicos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mecanico>>> GetMecanicos()
    {
        return Ok(await _servico.ListarAsync());
    }

    // GET: api/Mecanicos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Mecanico>> GetMecanico(int id)
    {
        var mecanico = await _servico.ObterAsync(id);

        if (mecanico is null)
        {
            return NotFound(); // null -> 404
        }

        return Ok(mecanico);
    }

    // POST: api/Mecanicos
    [HttpPost]
    public async Task<ActionResult<Mecanico>> PostMecanico(Mecanico mecanico)
    {
        try
        {
            var criado = await _servico.CriarAsync(mecanico);
            return CreatedAtAction(nameof(GetMecanico), new { id = criado.Id }, criado);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(ex.Message); // regra quebrada -> 400
        }
    }

    // PUT: api/Mecanicos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMecanico(int id, Mecanico mecanicoAtualizado)
    {
        try
        {
            var atualizado = await _servico.AtualizarAsync(id, mecanicoAtualizado);

            if (!atualizado)
            {
                return NotFound(); // false -> 404
            }

            return NoContent();
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Mecanicos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMecanico(int id)
    {
        var removido = await _servico.RemoverAsync(id);

        if (!removido)
        {
            return NotFound();
        }

        return NoContent();
    }
}