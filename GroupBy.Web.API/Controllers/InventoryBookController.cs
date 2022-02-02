using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.InventoryBook;
using GroupBy.Application.DTO.InventoryBookRecord;
using GroupBy.Application.DTO.InventoryItem;
using GroupBy.Application.Exceptions;
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
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class InventoryBookController : ControllerBase
    {
        private readonly IInventoryBookService inventoryBookService;

        public InventoryBookController(IInventoryBookService inventoryBookService)
        {
            this.inventoryBookService = inventoryBookService;
        }
        [HttpGet("", Name = "GetAllInventoryBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryBookSimpleDTO>>> GetAllAsync()
        {
            return Ok(await inventoryBookService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetInventoryBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryBookDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await inventoryBookService.GetAsync(new InventoryBookSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/records", Name = "GetInventoryBookRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InventoryBookRecordSimpleDTO>>> GetInventoryBookRecordsAsync(int id)
        {
            try
            {
                return Ok(await inventoryBookService.GetInventoryBookRecordsAsync(new InventoryBookSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/items", Name = "GetInventoryItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetInventoryItemsAsync(int id)
        {
            try
            {
                return Ok(await inventoryBookService.GetInventoryItemsAsync(new InventoryBookSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddNewInventoryBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryBookSimpleDTO>> CreateAsync([FromBody] InventoryBookCreateDTO model)
        {
            try
            {
                return Ok(await inventoryBookService.CreateAsync(model));
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
        [HttpPut("edit", Name = "UpdateInventoryBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryBookSimpleDTO>> UpdateAsync([FromBody] InventoryBookUpdateDTO model)
        {
            try
            {
                return Ok(await inventoryBookService.UpdateAsync(model));
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
        [HttpDelete("delete", Name = "DeleteInventoryBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await inventoryBookService.DeleteAsync(new InventoryBookSimpleDTO { Id = id });
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
