using GroupBy.Application.Design.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GroupBy.Application.Exceptions;
using System.Net.Mime;
using GroupBy.Application.ViewModels.Volunteer;
using GroupBy.Application.ViewModels.Group;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }
        [HttpGet(Name = "GetAllGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GroupViewModel>>> GetAllAsync()
        {
            return Ok(await groupService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupViewModel>> GetAsync(int id)
        {
            GroupViewModel group;
            try
            {
                group = await groupService.GetAsync(new GroupViewModel { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
            return Ok(group);
        }
        [HttpGet("members/{id}", Name = "GetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VolunteerSimpleViewModel>>> GetMembersAsync(int id)
        {
            try
            {
                return Ok(await groupService.GetVolunteersAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupViewModel>> CreateAsync([FromBody] GroupCreateViewModel model)
        {
            try
            {
                return Ok(await groupService.CreateAsync(model));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteGroup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await groupService.DeleteAsync(new GroupViewModel { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = id, e.Message });
            }
            return NoContent();
        }
        [HttpPost("members/add/{groupId}/{volunteerId}", Name = "AddMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddMember(int groupId, int volunteerId)
        {
            try
            {
                await groupService.AddMember(groupId, volunteerId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            return NoContent();
        }
        [HttpPut("update", Name = "UpdateGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupViewModel>> UpdateAsync([FromBody] GroupUpdateViewModel model)
        {
            try
            {
                return Ok(await groupService.UpdateAsync(model));
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
    }
}
