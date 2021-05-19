using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.ViewModels.InventoryItem;
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
        public async Task<ActionResult<InventoryItemViewModel>> CreateAsync([FromBody] InventoryItemCreateViewModel model)
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
    }
}
