using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.DTO.Position;
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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        [HttpPost("add", Name = "CreatePosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionSimpleDTO>> CreateAsync([FromBody] PositionCreateDTO model)
        {
            try
            {
                return Ok(await positionService.CreateAsync(model));
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
        [HttpGet("", Name = "GetAllPositions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PositionSimpleDTO>>> GetAllAsync()
        {
            return Ok(await positionService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionSimpleDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await positionService.GetAsync(new PositionSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPut("update", Name = "UpdatePosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionSimpleDTO>> UpdateAsync([FromBody] PositionSimpleDTO model)
        {
            try
            {
                return Ok(await positionService.UpdateAsync(model));
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
        [HttpDelete("delete/{id}", Name = "DeletePosition")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await positionService.DeleteAsync(new PositionSimpleDTO { Id = id });
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
