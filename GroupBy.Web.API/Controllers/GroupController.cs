using GroupBy.Design.Exceptions;
using GroupBy.Design.Services;
using GroupBy.Design.TO.AccountingBook;
using GroupBy.Design.TO.AccountingDocument;
using GroupBy.Design.TO.Document;
using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Project;
using GroupBy.Design.TO.Volunteer;
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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }
        [HttpGet(Name = "GetAllGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GroupSimpleDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await groupService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupDTO>> GetAsync(Guid id)
        {
            GroupDTO group;
            try
            {
                group = await groupService.GetAsync(new GroupSimpleDTO { Id = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            return Ok(group);
        }

        [HttpGet("{id}/members", Name = "GetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VolunteerSimpleDTO>>> GetMembersAsync(Guid id)
        {
            try
            {
                return Ok(await groupService.GetVolunteersAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupDTO>> CreateAsync([FromBody] GroupCreateDTO model)
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await groupService.DeleteAsync(new GroupSimpleDTO { Id = id });
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
        [HttpPost("members/add/{groupId}/{volunteerId}", Name = "AddMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMemberAsync(Guid groupId, Guid volunteerId)
        {
            try
            {
                await groupService.AddMemberAsync(groupId, volunteerId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpPost("members/remove/{groupId}/{volunteerId}", Name = "RemoveMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveMemberAsync(Guid groupId, Guid volunteerId)
        {
            try
            {
                await groupService.RemoveMemberAsync(groupId, volunteerId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpPut("update", Name = "UpdateGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupDTO>> UpdateAsync([FromBody] GroupUpdateDTO model)
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
        [HttpGet("{parentGroupId}/subgroups", Name = "GetSubgroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GroupSimpleDTO>>> GetSubgroupsAsync(Guid parentGroupId)
        {
            try
            {
                return Ok(await groupService.GetSubgroupsAsync(parentGroupId));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/accountingBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AccountingBookSimpleDTO>>> GetAccountingBooksAsync(Guid id)
        {
            try
            {
                return Ok(await groupService.GetAccountingBooksAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectSimpleDTO>>> GetProjectsAsync(Guid id)
        {
            try
            {
                return Ok(await groupService.GetProjectsAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/accountingDocuments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AccountingDocumentSimpleDTO>>> GetAccountingDocumentsAsync(Guid id, [FromQuery(Name = "project-id")] Guid? projectId)
        {
            try
            {
                return Ok(await groupService.GetAccountingDocumentsAsync(id, projectId));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpGet("{id}/documents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DocumentDTO>>> GetDocumentsAsync(Guid id, [FromQuery(Name = "project-id")] Guid? projectId)
        {
            try
            {
                return Ok(await groupService.GetDocumentsAsync(id, projectId));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
    }
}
