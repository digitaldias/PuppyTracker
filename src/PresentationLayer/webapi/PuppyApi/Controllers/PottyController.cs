using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuppyApi.Data;
using PuppyApi.Managers;
using PuppyApi.Models;

namespace PuppyApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PottyController : ControllerBase
    {
        private readonly IPottyBreakRepository _pottyBreakRepository;
        private readonly IExceptionHandler _exceptionHandler;

        public PottyController(IPottyBreakRepository pottyBreakRepository, IExceptionHandler exceptionHandler)
        {
            _pottyBreakRepository = pottyBreakRepository;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<PottyBreak>> GetPottyBreaks()
        {
            return  await _exceptionHandler.GetAsync(() => _pottyBreakRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Guid verifiedGuid;
            var couldParse = Guid.TryParseExact(id, "D", out verifiedGuid);
            if (!couldParse)
                return BadRequest();

            var pottyBreak = await _exceptionHandler.GetAsync(() => _pottyBreakRepository.GetById(verifiedGuid));
            if (pottyBreak != null)
                return BadRequest();

            return Ok(pottyBreak);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPottyBreakAsync([FromRoute] Guid id, [FromBody] PottyBreak pottyBreak)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != pottyBreak.Id)
                return BadRequest();

            await _exceptionHandler.RunAsync(() => _pottyBreakRepository.SaveAsync(pottyBreak));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pottyBreak = await _pottyBreakRepository.GetById(id);
            if (pottyBreak == null)
                return NotFound();

            await _exceptionHandler.RunAsync(() => _pottyBreakRepository.DeleteAsync(pottyBreak));
            return Ok();
        }
    }
}