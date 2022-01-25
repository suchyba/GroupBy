﻿using Microsoft.AspNetCore.Mvc;
using GroupBy.Application.Design.Services;
using System.Net.Mime;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GroupBy.Application.Exceptions;
using GroupBy.Application.DTO.Volunteer;
using GroupBy.Application.DTO.Group;

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
        public async Task<ActionResult<IEnumerable<VolunteerDTO>>> GetAllAsync()
        {
            return Ok(await volunteerService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VolunteerDTO>> GetVolunteerAsync(int id)
        {
            try
            {
                return Ok(await volunteerService.GetAsync(new VolunteerSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VolunteerDTO>> CreateAsync([FromBody] VolunteerCreateDTO model)
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await volunteerService.DeleteAsync(new VolunteerSimpleDTO { Id = id });
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
        [HttpPut("update", Name = "UpdateVolunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VolunteerDTO>> UpdateAsync([FromBody] VolunteerUpdateDTO model)
        {
            try
            {
                return Ok(await volunteerService.UpdateAsync(model));
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
        [HttpGet("{id}/groups", Name = "GetVoluneerGroups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GroupSimpleDTO>>> GetGroupsAsync(int id)
        {
            try
            {
                return Ok(await volunteerService.GetGroupsAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
    }
}
