using GroupBy.Application.Design.Services;
using GroupBy.Application.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GroupBy.Application.Exceptions;
using System.Net.Mime;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
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
    }
}
