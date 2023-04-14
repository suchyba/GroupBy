using GroupBy.Design.Services;
using GroupBy.Design.TO.InventoryItemSource;
using GroupBy.Design.Exceptions;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class InventoryItemSourceController : ControllerBase
    {
        private readonly IInventoryItemSourceService inventoryItemSourceService;

        public InventoryItemSourceController(IInventoryItemSourceService inventoryItemSourceService)
        {
            this.inventoryItemSourceService = inventoryItemSourceService;
        }
        [HttpGet("", Name = "GetAllInventoryItemSources")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryItemSourceDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await inventoryItemSourceService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetInventoryItemSource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryItemSourceDTO>> GetAsync(Guid id)
        {
            try
            {
                return Ok(await inventoryItemSourceService.GetAsync(new InventoryItemSourceDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateNewInventoryItemSource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryItemSourceDTO>> CreateAsync([FromBody] InventoryItemSourceCreateDTO source)
        {
            try
            {
                return Ok(await inventoryItemSourceService.CreateAsync(source));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpPut("update", Name = "UpdateInventoryItemSource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryItemSourceDTO>> UpdateAsync([FromBody] InventoryItemSourceDTO source)
        {
            try
            {
                return Ok(await inventoryItemSourceService.UpdateAsync(source));
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
        [HttpDelete("delete", Name = "DeleteIntentoryItemSource")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await inventoryItemSourceService.DeleteAsync(new InventoryItemSourceDTO { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            catch (DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
            return NoContent();
        }
    }
}
