using Microsoft.AspNetCore.Mvc;
using PROJ_P2.Models;
using PROJ_P2.Services;

namespace PROJ_P2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdemServicoController : ControllerBase
{
    private readonly IOrdemServicoService _service;

    public OrdemServicoController(IOrdemServicoService service)
    {
        _service = service;
    }

    // GET: api/OrdemServico
    [HttpGet]
    public async Task<ActionResult<List<OrdemServico>>> Listar()
    {
        var ordens = await _service.ListarAsync();
        return Ok(ordens);
    }

    // GET: api/OrdemServico/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrdemServico>> Obter(int id)
    {
        var ordem = await _service.ObterAsync(id);

        if (ordem is null)
        {
            return NotFound();
        }

        return Ok(ordem);
    }

    // POST: api/OrdemServico
    [HttpPost]
    public async Task<ActionResult<OrdemServico>> Abrir([FromBody] OrdemServico ordemServico)
    {
        try
        {
            var criada = await _service.AbrirAsync(ordemServico);
            return CreatedAtAction(nameof(Obter), new { id = criada.Id }, criada);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/OrdemServico/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<OrdemServico>> Atualizar(int id, [FromBody] OrdemServico ordemServico)
    {
        try
        {
            ordemServico.Id = id;
            var atualizada = await _service.AtualizarAsync(ordemServico);

            if (atualizada is null)
            {
                return NotFound();
            }

            return Ok(atualizada);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PATCH: api/OrdemServico/5/fechar
    [HttpPatch("{id:int}/fechar")]
    public async Task<ActionResult<OrdemServico>> FecharOrdem(int id)
    {
        try
        {
            var fechada = await _service.FecharOrdemAsync(id);

            if (fechada is null)
            {
                return NotFound();
            }

            return Ok(fechada);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // DELETE: api/OrdemServico/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removida = await _service.RemoverAsync(id);

        if (!removida)
        {
            return NotFound();
        }

        return NoContent();
    }
}