using Microsoft.AspNetCore.Mvc;
using PROJ_P2.Models;
using PROJ_P2.Services;

namespace PROJ_P2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarroController : ControllerBase
{
    private readonly ICarroService _service;

    public CarroController(ICarroService service)
    {
        _service = service;
    }

    // GET: api/Carro
    [HttpGet]
    public async Task<ActionResult<List<Carro>>> Listar()
    {
        var carros = await _service.ListarAsync();
        return Ok(carros);
    }

    // GET: api/Carro/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Carro>> Obter(int id)
    {
        var carro = await _service.ObterAsync(id);

        if (carro is null)
        {
            return NotFound();
        }

        return Ok(carro);
    }

    // POST: api/Carro
    [HttpPost]
    public async Task<ActionResult<Carro>> Cadastrar([FromBody] Carro carro)
    {
        try
        {
            var criado = await _service.CadastrarAsync(carro);
            return CreatedAtAction(nameof(Obter), new { id = criado.Id }, criado);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/Carro/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Carro>> Atualizar(int id, [FromBody] Carro carro)
    {
        try
        {
            carro.Id = id;
            var atualizado = await _service.AtualizarAsync(carro);

            if (atualizado is null)
            {
                return NotFound();
            }

            return Ok(atualizado);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // DELETE: api/Carro/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _service.RemoverAsync(id);

        if (!removido)
        {
            return NotFound();
        }

        return NoContent();
    }
}