using GroupBy.Design.Exceptions;
using GroupBy.Design.Services;
using GroupBy.Design.DTO.AccountingBook;
using GroupBy.Design.DTO.FinancialRecord;
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
    public class AccountingBookController : ControllerBase
    {
        private readonly IAccountingBookService accountingBookService;

        public AccountingBookController(IAccountingBookService accountingBookService)
        {
            this.accountingBookService = accountingBookService;
        }

        [HttpGet(Name = "GetAllAccountingBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountingBookSimpleDTO>>> GetAllAsync()
        {
            return Ok(await accountingBookService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookDTO>> GetAsync(Guid id)
        {
            try
            {
                var accountingBook = await accountingBookService.GetAsync(new AccountingBookSimpleDTO { Id = id });
                return Ok(accountingBook);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/records", Name = "GetFinancialRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FinancialRecordSimpleDTO>>> GetFinancialRecordsAsync(Guid id)
        {
            try
            {
                var records = await accountingBookService.GetFinancialRecordsAsync(new AccountingBookSimpleDTO { Id = id });
                return Ok(records);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingBookDTO>> CreateAsync([FromBody] AccountingBookCreateDTO model)
        {
            try
            {
                var accountingBook = await accountingBookService.CreateAsync(model);
                return Ok(accountingBook);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteAccountingBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await accountingBookService.DeleteAsync(new AccountingBookSimpleDTO { Id = id });

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
        [HttpPut("edit", Name = "EditAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookSimpleDTO>> Edit([FromBody] AccountingBookSimpleDTO model)
        {
            try
            {
                var accountingBook = await accountingBookService.UpdateAsync(model);
                return Ok(accountingBook);
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
