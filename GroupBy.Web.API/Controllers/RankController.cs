using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.DTO.Rank;
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
        public async Task<ActionResult<IEnumerable<RankDTO>>> GetAllAsync()
        {
            return Ok(await rankService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetRank")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RankDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await rankService.GetAsync(new RankDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
                throw;
            }
        }
        [HttpPost("add", Name = "CreateRank")]
        public async Task<ActionResult<RankDTO>> CreateAsync([FromBody] RankCreateDTO model)
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
        public async Task<ActionResult<RankDTO>> UpdateAsync([FromBody] RankDTO model)
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await rankService.DeleteAsync(new RankDTO { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch(DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
            return NoContent();
        }
    }
}
