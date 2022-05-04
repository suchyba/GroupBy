using GroupBy.Application.Design.Services;
using GroupBy.Application.DTO.FinancialRecord;
using GroupBy.Application.DTO.Project;
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
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService service)
        {
            this.service = service;
        }
        [HttpGet("", Name = "GetAllProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectSimpleDTO>>> getAllAsync()
        {
            return Ok(await service.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDTO>> GetAsync(int id)
        {
            try
            {
                return Ok(await service.GetAsync(new ProjectSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "CreateProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectSimpleDTO>> CreateAsync([FromBody] ProjectCreateDTO project)
        {
            try
            {
                return Ok(await service.CreateAsync(project));
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
        [HttpPut("update", Name = "UpdateProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectSimpleDTO>> UpdateAsync([FromBody] ProjectUpdateDTO project)
        {
            try
            {
                return Ok(await service.UpdateAsync(project));
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
        [HttpDelete("delete/{id}", Name = "DeleteProject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await service.DeleteAsync(new ProjectSimpleDTO { Id = id });
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
        [HttpGet("{id}/financialRecords", Name = "GetFinancialRecordsRelatedToProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FinancialRecordSimpleDTO>>> GetRelatedFinancialRecordsAsync(int id)
        {
            try
            {
                return Ok(await service.GetRelatedFinancialRecordsAsync(new ProjectSimpleDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
    }
}
