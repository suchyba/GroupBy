using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountingBookController : ControllerBase
    {
        private readonly IAccountingBookAsyncService accountingBookService;

        public AccountingBookController(IAccountingBookAsyncService accountingBookService)
        {
            this.accountingBookService = accountingBookService;
        }

        [HttpGet(Name = "GetAllAccountingBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountingBookViewModel>>> GetAllAsync()
        {
            return Ok(await accountingBookService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookViewModel>> GetAsync(int bookId, int bookOrderNumberId)
        {
            try
            {
                var accountingBook = await accountingBookService.GetAsync(new AccountingBookViewModel { BookId = bookId, BookOrderNumberId = bookOrderNumberId });
                return Ok(accountingBook);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = new { bookId, bookOrderNumberId }, e.Message });
            }
        }
        [HttpPost("add", Name = "AddAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingBookViewModel>> CreateAsync([FromBody] AccountingBookViewModel model)
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
        [HttpDelete("delete/{bookId}/{bookOrderNumberId}", Name = "DeleteAccountingBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int bookId, int bookOrderNumberId)
        {
            try
            {
                await accountingBookService.DeleteAsync(new AccountingBookViewModel { BookId = bookId, BookOrderNumberId = bookOrderNumberId });

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = new { bookId, bookOrderNumberId }, e.Message });
            }
        }
        [HttpPut("edit", Name = "EditAccountingBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookViewModel>> Edit([FromBody] AccountingBookViewModel model)
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
                return NotFound(new { Id = new { model.BookId, model.BookOrderNumberId }, e.Message });
            }
        }
    }
}
