using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.AccountingDocument;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class AccountingDocumentController : ControllerBase
    {
        private readonly IAccountingDocumentService accountingDocumentService;

        public AccountingDocumentController(IAccountingDocumentService accountingDocumentService)
        {
            this.accountingDocumentService = accountingDocumentService;
        }
        [HttpGet(Name = "GetAllAccountingDocuments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccountingDocumentSimpleDTO>>> GetAllAsync()
        {
            return Ok(await accountingDocumentService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetAccountingDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingDocumentSimpleDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await accountingDocumentService.GetAsync(new AccountingDocumentSimpleDTO { Id = id }));
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
        [HttpPut("edit", Name = "UpdateAccountingDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingDocumentSimpleDTO>> UpdateAsync(AccountingDocumentSimpleDTO DTO)
        {
            try
            {
                return Ok(await accountingDocumentService.UpdateAsync(DTO));
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
        [HttpPost("add", Name = "CreateAccountingDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountingDocumentSimpleDTO>> CreateAsync(AccountingDocumentCreateDTO DTO)
        {
            try
            {
                return Ok(await accountingDocumentService.CreateAsync(DTO));
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
        [HttpDelete("delete", Name = "DeleteAccountingDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await accountingDocumentService.DeleteAsync(new AccountingDocumentSimpleDTO { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            return NoContent();
        }
    }
}
