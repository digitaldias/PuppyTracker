using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuppyApi.Data;
using PuppyApi.Domain.Contracts.Handlers;
using PuppyApi.Domain.Contracts.Managers;
using PuppyApi.Domain.Entities;

namespace PuppyApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PottyController : ControllerBase
    {
        private readonly IPottyBreaksManager _pottyBreaksManager;

        public PottyController(IPottyBreaksManager pottyBreaksManager)
        {
            if (pottyBreaksManager is null)
                throw new ArgumentNullException(nameof(pottyBreaksManager));

            _pottyBreaksManager = pottyBreaksManager;
        }

        [HttpGet]
        public async Task<IEnumerable<PottyBreak>> GetPottyBreaks()
        {
            return await _pottyBreaksManager.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pottyBreak = await _pottyBreaksManager.GetByIdAsync(id);
            if (pottyBreak is null)
                return NotFound();

            return Ok(pottyBreak);
        }

        [HttpPost]
        public async Task<IActionResult> PutPottyBreakAsync([FromBody] PottyBreak pottyBreak)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            await _pottyBreaksManager.SaveAsync(pottyBreak);

            return CreatedAtAction(nameof(GetAsync), pottyBreak);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pottyBreak = await _pottyBreaksManager.GetByIdAsync(id);
            if (pottyBreak is null)
                return NotFound();

            await _pottyBreaksManager.DeleteAsync(pottyBreak);

            return NoContent();
        }
    }
}