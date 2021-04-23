using GroupBy.Design.Maps;
using GroupBy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [Route("/api/[controller]")]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerMap map;
        public VolunteerController(IVolunteerMap map)
        {
            this.map = map;
        }
        [HttpGet]
        public IEnumerable<VolunteerViewModel> GetAll()
        {
            return map.GetAll();
        }
        [HttpGet]
        [Route("{id}")]
        public VolunteerViewModel Get(string id)
        {
            return map.Get(new VolunteerViewModel { Id = id });
        }
        [HttpPost]
        [Route("add")]
        public VolunteerViewModel Create([FromBody] VolunteerViewModel model)
        {
            return map.Create(model);
        }
        [HttpDelete]
        [Route("delete")]
        public bool Delete([FromBody] VolunteerViewModel model)
        {
            return map.Delete(model);
        }
        [HttpPut]
        [Route("edit")]
        public bool Edit([FromBody] VolunteerViewModel model)
        {
            return map.Update(model);
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
