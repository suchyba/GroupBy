using GroupBy.Design.DTO.InventoryItemTransfer;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class InventoryItemTransferController : ControllerBase
    {
        private readonly IInventoryItemTransferService inventoryItemTransferService;

        public InventoryItemTransferController(IInventoryItemTransferService inventoryItemTransferService)
        {
            this.inventoryItemTransferService = inventoryItemTransferService;
        }

        [HttpGet("", Name = "GetAllInventoryBookTransfers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InventoryItemTransferSimpleDTO>>> GetAllAsync()
        {
            return Ok(await inventoryItemTransferService.GetAllAsync());
        }

        [HttpPost("confirm", Name = "ConfirmInventoryItemTransfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryItemTransferDTO>> ConfirmTransferAsync([FromBody] InventoryItemTransferConfirmDTO transferConfirmation)
        {
            try
            {
                return Ok(await inventoryItemTransferService.ConfirmTransferAsync(transferConfirmation));
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

        [HttpPost("add", Name = "CreateInventoryItemTransfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryItemTransferDTO>> CreateAsync([FromBody] InventoryItemTransferCreateDTO createDTO)
        {
            try
            {
                return Ok(await inventoryItemTransferService.CreateAsync(createDTO));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateInventoryItemTransfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InventoryItemTransferDTO>> UpdateAsync([FromBody] InventoryItemTransferUpdateDTO updateDTO)
        {
            try
            {
                return Ok(await inventoryItemTransferService.UpdateAsync(updateDTO));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/cancel", Name = "CancelInventoryItemTransfer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CancelAsync(Guid id)
        {
            try
            {
                await inventoryItemTransferService.CancelTransferAsync(id);
                return NoContent();
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
