using GroupBy.Design.Services;
using GroupBy.Design.TO.InventoryBookRecord;
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
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class InventoryBookRecordController : ControllerBase
    {
        private readonly IInventoryBookRecordService InventoryBookRecordService;

        public InventoryBookRecordController(IInventoryBookRecordService InventoryBookRecordService)
        {
            this.InventoryBookRecordService = InventoryBookRecordService;
        }
        [HttpGet("", Name = "GetAllInventoryBookRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryBookRecordSimpleDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await InventoryBookRecordService.GetAllAsync());
        }
        [HttpGet("{inventoryBookId}/{id}", Name = "GetInventoryBookRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryBookRecordDTO>> GetAsync(Guid inventoryBookId, Guid id)
        {
            try
            {
                return Ok(await InventoryBookRecordService.GetAsync(new InventoryBookRecordSimpleDTO { Id = id, InventoryBookId = inventoryBookId }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddNewInventoryBookRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryBookRecordDTO>> CreateAsync([FromBody] InventoryBookRecordCreateDTO model)
        {
            try
            {
                return Ok(await InventoryBookRecordService.CreateAsync(model));
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
        [HttpPut("edit", Name = "UpdateInventoryBookRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryBookRecordDTO>> UpdateAsync([FromBody] InventoryBookRecordUpdateDTO model)
        {
            try
            {
                return Ok(await InventoryBookRecordService.UpdateAsync(model));
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
        [HttpDelete("delete/{id}", Name = "DeleteInventoryBookRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await InventoryBookRecordService.DeleteAsync(new InventoryBookRecordSimpleDTO { Id = id });
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
        [HttpPost("transfer", Name = "TransferInventoryItemBetweenBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryBookRecordDTO>> TransferItemAsync([FromBody] InventoryBookRecordTransferDTO model)
        {
            try
            {
                return Ok(await InventoryBookRecordService.TransferItemAsync(model));
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
    }
}
