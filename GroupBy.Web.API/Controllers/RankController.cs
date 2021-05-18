using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.ViewModels.Rank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RankController : ControllerBase
    {
        private readonly IRankService rankService;

        public RankController(IRankService rankService)
        {
            this.rankService = rankService;
        }
        [HttpGet("", Name = "GetAllRanks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RankViewModel>>> GetAllAsync()
        {
            return Ok(await rankService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RankViewModel>> GetAsync(int id)
        {
            try
            {
                return Ok(await rankService.GetAsync(new RankViewModel { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
                throw;
            }
        }
        [HttpPost("add", Name = "CreateRank")]
        public async Task<ActionResult<RankViewModel>> CreateAsync([FromBody] RankCreateViewModel model)
        {
            try
            {
                return Ok(await rankService.CreateAsync(model));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPut("update", Name = "UpdateRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RankViewModel>> UpdateAsync([FromBody] RankViewModel model)
        {
            try
            {
                return Ok(await rankService.UpdateAsync(model));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteRank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await rankService.DeleteAsync(new RankViewModel { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
            return NoContent();
        }
    }
}
