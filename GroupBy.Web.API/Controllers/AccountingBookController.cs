using GroupBy.Application.Design.Services;
using GroupBy.Application.Exceptions;
using GroupBy.Application.Responses;
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
        public async Task<ActionResult<AccountingBookViewModel>> GetAsync(Guid id)
        {
            try
            {
                return Ok(await accountingBookService.GetAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost("add")]
        public async Task<ActionResult<AccountingBookResponse>> CreateAsync([FromBody] AccountingBookViewModel model)
        {
            var response = new AccountingBookResponse();
            try
            {
                var accountingBook = await accountingBookService.CreateAsync(model);
                response.Succes = true;
                response.accountingBook = accountingBook;
                return Ok(response);
            }
            catch (ValidationException e)
            {
                response.Succes = false;
                foreach (var error in e.ValidationErrors)
                {
                    response.ValidationErrors.Add(error);
                }
                return BadRequest(response);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await accountingBookService.DeleteAsync(id);
                
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPut("edit")]
        public async Task<ActionResult<AccountingBookResponse>> Edit([FromBody] AccountingBookViewModel model)
        {
            var response = new AccountingBookResponse();
            try
            {
                var accountingBook = await accountingBookService.UpdateAsync(model);
                response.Succes = true;
                response.accountingBook = accountingBook;
                return Ok(response);
            }
            catch (ValidationException e)
            {
                response.Succes = false;
                foreach (var error in e.ValidationErrors)
                {
                    response.ValidationErrors.Add(error);
                }
                return BadRequest(response);
            }
            catch(NotFoundException e)
            {
                response.Succes = false;
                response.Message = e.Message;
                return NotFound(response);
            }
        }
    }
}
