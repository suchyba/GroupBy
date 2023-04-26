using GroupBy.Design.Services;
using GroupBy.Design.DTO.RegistrationCode;
using GroupBy.Design.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class RegistrationCodeController : ControllerBase
    {
        private readonly IRegistrationCodeService registrationCodeService;

        public RegistrationCodeController(IRegistrationCodeService registrationCodeService)
        {
            this.registrationCodeService = registrationCodeService;
        }

        [HttpGet(Name = "GetAllRegistrationCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RegistrationCodeSimpleDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await registrationCodeService.GetAllAsync());
        }
        [HttpGet("{code}", Name = "GetRegistrationCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistrationCodeFullDTO>> GetAsync(string code)
        {
            try
            {
                var registrationCode = await registrationCodeService.GetAsync(new RegistrationCodeSimpleDTO { Code = code});
                return Ok(registrationCode);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddRegistrationCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegistrationCodeFullDTO>> CreateAsync([FromBody] RegistrationCodeCreateDTO model)
        {
            try
            {
                var registrationCode = await registrationCodeService.CreateAsync(model);
                return Ok(registrationCode);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{code}", Name = "DeleteRegistrationCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(string code)
        {
            try
            {
                await registrationCodeService.DeleteAsync(new RegistrationCodeSimpleDTO { Code = code });

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
    }
}
