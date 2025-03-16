using GroupBy.Design.DTO.AccountingBookTemplate;
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
    public class AccountingBookTemplateController : ControllerBase
    {
        private readonly IAccountingBookTemplateService accountingBookTemplateService;

        public AccountingBookTemplateController(IAccountingBookTemplateService accountingBookTemplateService)
        {
            this.accountingBookTemplateService = accountingBookTemplateService;
        }

        [HttpGet(Name = "GetAllAccountingBookTemplates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountingBookTemplateSimpleDTO>>> GetAllAsync()
        {
            return Ok(await accountingBookTemplateService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetAccountingBookTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookTemplateDTO>> GetAsync(Guid id)
        {
            try
            {
                var accountingBook = await accountingBookTemplateService.GetAsync(new AccountingBookTemplateSimpleDTO { Id = id });
                return Ok(accountingBook);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }        
        [HttpPost("add", Name = "AddAccountingBookTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingBookTemplateDTO>> CreateAsync([FromBody] AccountingBookTemplateCreateDTO model)
        {
            try
            {
                var accountingBook = await accountingBookTemplateService.CreateAsync(model);
                return Ok(accountingBook);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteAccountingBookTemplate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await accountingBookTemplateService.DeleteAsync(new AccountingBookTemplateSimpleDTO { Id = id });

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
        [HttpPut("edit", Name = "EditAccountingBookTemplate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountingBookTemplateSimpleDTO>> Edit([FromBody] AccountingBookTemplateUpdateDTO model)
        {
            try
            {
                var accountingBook = await accountingBookTemplateService.UpdateAsync(model);
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
