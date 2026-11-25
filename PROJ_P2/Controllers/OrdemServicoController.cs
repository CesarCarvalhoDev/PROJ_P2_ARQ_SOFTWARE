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