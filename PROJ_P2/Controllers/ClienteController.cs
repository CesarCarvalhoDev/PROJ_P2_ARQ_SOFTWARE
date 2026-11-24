using Microsoft.AspNetCore.Mvc;
using PROJ_P2.Models;
using PROJ_P2.Services;

namespace PROJ_P2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    // GET: api/Cliente
    [HttpGet]
    public async Task<ActionResult<List<Cliente>>> Listar()
    {
        var clientes = await _service.ListarAsync();
        return Ok(clientes);
    }

    // GET: api/Cliente/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente>> Obter(int id)
    {
        var cliente = await _service.ObterAsync(id);

        if (cliente is null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    // POST: api/Cliente
    [HttpPost]
    public async Task<ActionResult<Cliente>> Cadastrar([FromBody] Cliente cliente)
    {
        try
        {
            var criado = await _service.CadastrarAsync(cliente);
            return CreatedAtAction(nameof(Obter), new { id = criado.Id }, criado);
        }
        catch (RegraDeNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // PUT: api/Cliente/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Cliente>> Atualizar(int id, [FromBody] Cliente cliente)
    {
        try
        {
            cliente.Id = id;
            var atualizado = await _service.AtualizarAsync(cliente);

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

    // DELETE: api/Cliente/5
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