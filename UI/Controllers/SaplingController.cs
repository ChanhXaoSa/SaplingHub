﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH_BusinessObjects.Common.Model.Sapling;
using SH_BusinessObjects.Entities;
using SH_Services.Services.Interfaces;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaplingController(ISaplingService saplingService) : ControllerBase
    {
        private readonly ISaplingService _saplingService = saplingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var saplings = await _saplingService.GetAllAsync();
            return Ok(saplings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sapling = await _saplingService.GetByIdAsync(id);
            if (sapling == null)
                return NotFound();
            return Ok(sapling);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaplingModel sapling)
        {
            try
            {
                var createdSapling = await _saplingService.CreateAsync(sapling);
                return CreatedAtAction(nameof(GetById), new { id = createdSapling.Id }, createdSapling);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SaplingModel sapling)
        {
            try
            {
                await _saplingService.UpdateAsync(id, sapling);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _saplingService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
