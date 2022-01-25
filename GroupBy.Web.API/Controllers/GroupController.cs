﻿using GroupBy.Application.Design.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GroupBy.Application.Exceptions;
using System.Net.Mime;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Application.DTO.Group;

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
        public async Task<ActionResult<List<GroupDTO>>> GetAllAsync()
        {
            return Ok(await groupService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupSimpleDTO>> GetAsync(int id)
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
        [HttpGet("members/{id}", Name = "GetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VolunteerSimpleDTO>>> GetMembersAsync(int id)
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
        public async Task<ActionResult> DeleteAsync(int id)
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
        public async Task<ActionResult> AddMemberAsync(int groupId, int volunteerId)
        {
            try
            {
                await groupService.AddMemberAsync(groupId, volunteerId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Key, e.Message });
            }
            return NoContent();
        }
        [HttpPost("members/remove/{groupId}/{volunteerId}", Name = "RemoveMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveMemberAsync(int groupId, int volunteerId)
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
        public async Task<ActionResult<IEnumerable<GroupSimpleDTO>>> GetSubgroupsAsync(int parentGroupId)
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
    }
}
