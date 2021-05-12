using Microsoft.AspNetCore.Mvc;
using GroupBy.Application.Design.Services;
using System.Net.Mime;
using GroupBy.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GroupBy.Application.Exceptions;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/api/[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }
        [HttpGet("", Name = "GetAllVolunteers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VolunteerViewModel>>> GetAllAsync()
        {
            return Ok(await volunteerService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VolunteerViewModel>> GetVolunteerAsync(int id)
        {
            try
            {
                return Ok(await volunteerService.GetAsync(new VolunteerViewModel { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VolunteerViewModel>> CreateAsync([FromBody] VolunteerCreateViewModel model)
        {
            try
            {
                return Ok(await volunteerService.CreateAsync(model));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteVolunteer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await volunteerService.DeleteAsync(new VolunteerViewModel { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
            return NoContent();
        }
        [HttpPut("update", Name = "UpdateVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VolunteerViewModel>> UpdateAsync([FromBody] VolunteerUpdateViewModel model)
        {
            try
            {
                return Ok(await volunteerService.UpdateAsync(model));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = model.Id, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpGet("{id}/groups", Name = "GetVoluneerGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetGroupsAsync(int id)
        {
            try
            {
                return Ok(await volunteerService.GetGroupsAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
        }
    }
}
