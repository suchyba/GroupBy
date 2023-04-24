using GroupBy.Design.Services;
using GroupBy.Design.TO.FinancialOutcomeRecord;
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
    public class FinancialOutcomeRecordController : ControllerBase
    {
        private readonly IFinancialOutcomeRecordService FinancialOutcomeRecordService;

        public FinancialOutcomeRecordController(IFinancialOutcomeRecordService FinancialOutcomeRecordService)
        {
            this.FinancialOutcomeRecordService = FinancialOutcomeRecordService;
        }

        [HttpGet(Name = "GetAllFinancialOutcomeRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialOutcomeRecordSimpleDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await FinancialOutcomeRecordService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOutcomeRecordDTO>> GetAsync(Guid id)
        {
            try
            {
                var FinancialOutcomeRecord = await FinancialOutcomeRecordService.GetAsync(new FinancialOutcomeRecordSimpleDTO { Id = id });
                return Ok(FinancialOutcomeRecord);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialOutcomeRecordSimpleDTO>> CreateAsync([FromBody] FinancialOutcomeRecordCreateDTO model)
        {
            try
            {
                var FinancialOutcomeRecord = await FinancialOutcomeRecordService.CreateAsync(model);
                return Ok(FinancialOutcomeRecord);
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
        [HttpDelete("delete/{id}", Name = "DeleteFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await FinancialOutcomeRecordService.DeleteAsync(new FinancialOutcomeRecordSimpleDTO { Id = id });

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
        [HttpPut("edit", Name = "EditFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOutcomeRecordSimpleDTO>> Edit([FromBody] FinancialOutcomeRecordUpdateDTO model)
        {
            try
            {
                var FinancialOutcomeRecord = await FinancialOutcomeRecordService.UpdateAsync(model);
                return Ok(FinancialOutcomeRecord);
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
