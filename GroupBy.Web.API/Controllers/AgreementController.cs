using GroupBy.Design.Services;
using GroupBy.Design.Exceptions;
using GroupBy.Design.DTO.Agreement;
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
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService agreementService;

        public AgreementController(IAgreementService agreementService)
        {
            this.agreementService = agreementService;
        }
        [HttpGet("", Name = "GetAllAgreements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AgreementDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await agreementService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgreementDTO>> GetAsync(Guid id)
        {
            try
            {
                return Ok(await agreementService.GetAsync(new AgreementDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddNewAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgreementDTO>> CreateAsync([FromBody] AgreementCreateDTO model)
        {
            try
            {
                return Ok(await agreementService.CreateAsync(model));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpPut("update", Name = "UpdateAgreement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgreementDTO>> UpdateAsync([FromBody] AgreementDTO model)
        {
            try
            {
                return Ok(await agreementService.UpdateAsync(model));
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
        [HttpDelete("delete/{id}", Name = "DeleteAgreement")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await agreementService.DeleteAsync(new AgreementDTO { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
            return NoContent();
        }
    }
}
