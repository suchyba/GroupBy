using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.FinancialIncomeRecord;
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
    public class FinancialIncomeRecordController : ControllerBase
    {
        private readonly IFinancialIncomeRecordService FinancialIncomeRecordService;

        public FinancialIncomeRecordController(IFinancialIncomeRecordService FinancialIncomeRecordService)
        {
            this.FinancialIncomeRecordService = FinancialIncomeRecordService;
        }

        [HttpGet(Name = "GetAllFinancialIncomeRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialIncomeRecordSimpleDTO>>> GetAllAsync()
        {
            return Ok(await FinancialIncomeRecordService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetFinancialIncomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialIncomeRecordSimpleDTO>> GetAsync(int id)
        {
            try
            {
                var FinancialIncomeRecord = await FinancialIncomeRecordService.GetAsync(new FinancialIncomeRecordSimpleDTO { Id = id });
                return Ok(FinancialIncomeRecord);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddFinancialIncomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialIncomeRecordSimpleDTO>> CreateAsync([FromBody] FinancialIncomeRecordCreateDTO model)
        {
            try
            {
                var FinancialIncomeRecord = await FinancialIncomeRecordService.CreateAsync(model);
                return Ok(FinancialIncomeRecord);
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
        [HttpDelete("delete/{id}", Name = "DeleteFinancialIncomeRecord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await FinancialIncomeRecordService.DeleteAsync(new FinancialIncomeRecordSimpleDTO { Id = id });

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
        [HttpPut("edit", Name = "EditFinancialIncomeRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialIncomeRecordSimpleDTO>> Edit([FromBody] FinancialIncomeRecordUpdateDTO model)
        {
            try
            {
                var FinancialIncomeRecord = await FinancialIncomeRecordService.UpdateAsync(model);
                return Ok(FinancialIncomeRecord);
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
