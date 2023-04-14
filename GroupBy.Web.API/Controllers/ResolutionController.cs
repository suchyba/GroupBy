using GroupBy.Design.Services;
using GroupBy.Design.TO.Resolution;
using GroupBy.Design.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using System;

namespace GroupBy.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ResolutionController : ControllerBase
    {
        private readonly IResolutionService resolutionService;

        public ResolutionController(IResolutionService resolutionService)
        {
            this.resolutionService = resolutionService;
        }

        [HttpGet(Name = "GetAllResolutions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResolutionDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await resolutionService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetResolution")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResolutionDTO>> GetAsync(Guid id)
        {
            try
            {
                var resolution = await resolutionService.GetAsync(new ResolutionDTO { Id = id });
                return Ok(resolution);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddResolution")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResolutionDTO>> CreateAsync([FromBody] ResolutionCreateDTO model)
        {
            try
            {
                var resolution = await resolutionService.CreateAsync(model);
                return Ok(resolution);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteResolution")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await resolutionService.DeleteAsync(new ResolutionDTO { Id = id });

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
        [HttpPut("edit", Name = "EditResolution")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResolutionDTO>> Edit([FromBody] ResolutionUpdateDTO model)
        {
            try
            {
                var resolution = await resolutionService.UpdateAsync(model);
                return Ok(resolution);
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
    }
}
