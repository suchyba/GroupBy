using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.FinancialOutcomeRecord;
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
    public class FinancialOutcomeRecordController : ControllerBase
    {
        private readonly IFinancialOutcomeRecordService FinancialOutcomeRecordService;

        public FinancialOutcomeRecordController(IFinancialOutcomeRecordService FinancialOutcomeRecordService)
        {
            this.FinancialOutcomeRecordService = FinancialOutcomeRecordService;
        }

        [HttpGet(Name = "GetAllFinancialOutcomeRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialOutcomeRecordDTO>>> GetAllAsync()
        {
            return Ok(await FinancialOutcomeRecordService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOutcomeRecordDTO>> GetAsync(int id)
        {
            try
            {
                var FinancialOutcomeRecord = await FinancialOutcomeRecordService.GetAsync(new FinancialOutcomeRecordDTO { Id = id });
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
        public async Task<ActionResult<FinancialOutcomeRecordDTO>> CreateAsync([FromBody] FinancialOutcomeRecordCreateDTO model)
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
        }
        [HttpDelete("delete/{id}", Name = "DeleteFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await FinancialOutcomeRecordService.DeleteAsync(new FinancialOutcomeRecordDTO { Id = id });

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPut("edit", Name = "EditFinancialOutcomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOutcomeRecordDTO>> Edit([FromBody] FinancialOutcomeRecordUpdateDTO model)
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
        }
    }
}
