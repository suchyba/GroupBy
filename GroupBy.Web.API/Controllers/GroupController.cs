using GroupBy.Design.Maps;
using GroupBy.Design.Services;
using GroupBy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupMap groupMap;
        private readonly IVolunteerMap volunteerMap;
        private readonly IGroupService groupService;

        public GroupController(IGroupMap groupMap, IVolunteerMap volunteerMap, IGroupService groupService)
        {
            this.groupMap = groupMap;
            this.volunteerMap = volunteerMap;
            this.groupService = groupService;
        }
        [HttpGet]
        public IEnumerable<GroupViewModel> GetAll()
        {
            return groupMap.GetAll();
        }
        [HttpGet]
        [Route("{id:int}")]
        public GroupViewModel Get(int id)
        {
            return groupMap.Get(new GroupViewModel { Id = id });
        }
        [HttpPost]
        [Route("add")]
        public GroupViewModel Create([FromBody] GroupViewModel model)
        {
            return groupMap.Create(model);
        }
        [HttpDelete]
        [Route("delete")]
        public bool Delete([FromBody] GroupViewModel model)
        {
            return groupMap.Delete(model);
        }
        [HttpPut]
        [Route("edit")]
        public bool Edit([FromBody] GroupViewModel model)
        {
            return groupMap.Update(model);
        }
        [HttpGet]
        [Route("volunteers")]
        public IEnumerable<VolunteerViewModel> GetVolunteers([FromBody] GroupViewModel model)
        {
            return volunteerMap.DomainToViewModel(groupService.GetVolunteers(groupMap.ViewModelToDomain(model)));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
