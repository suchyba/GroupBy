using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.DTO.InventoryItem;
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
    public class InventoryItemController : ControllerBase
    {
        private readonly IInventoryItemService inventoryItemService;

        public InventoryItemController(IInventoryItemService inventoryItemService)
        {
            this.inventoryItemService = inventoryItemService;
        }
        [HttpPost("add", Name = "CreateInventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryItemDTO>> CreateAsync([FromBody] InventoryItemCreateDTO model)
        {
            try
            {
                return Ok(await inventoryItemService.CreateAsync(model));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpGet("", Name = "GetAllInventoryItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAllAsync()
        {
            return Ok(await inventoryItemService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetInventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryItemDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await inventoryItemService.GetAsync(new InventoryItemDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPut("update", Name = "UpdateInventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryItemDTO>> UpdateAsync([FromBody] InventoryItemDTO model)
        {
            try
            {
                return Ok(await inventoryItemService.UpdateAsync(model));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch(ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteInventoryItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await inventoryItemService.DeleteAsync(new InventoryItemDTO { Id = id });
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
