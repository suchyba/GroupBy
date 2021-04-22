using GroupBy.Design.Maps;
using GroupBy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupMap map;
        public GroupController(IGroupMap _map)
        {
            map = _map;
        }
        [HttpGet]
        [Route("/groups/get")]
        public IEnumerable<GroupViewModel> GetAll()
        {
            return map.GetAll();
        }
        [HttpGet]
        [Route("/groups/get/{id:int}")]
        public GroupViewModel Get(int id)
        {
            return map.Get(id);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
