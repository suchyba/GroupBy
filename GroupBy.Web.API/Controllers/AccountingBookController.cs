using GroupBy.Design.Maps;
using GroupBy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountingBookController : Controller
    {
        private readonly IAccountingBookMap accountingBookMap;

        public AccountingBookController(IAccountingBookMap accountingBookMap)
        {
            this.accountingBookMap = accountingBookMap;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IEnumerable<AccountingBookViewModel> GetAll()
        {
            return accountingBookMap.GetAll();
        }
        [HttpGet]
        [Route("{id:int}/{orderNumber:int}")]
        public AccountingBookViewModel Get(int id, int orderNumber)
        {
            return accountingBookMap.Get(new AccountingBookViewModel { BookId = id , BookOrderNumberId = orderNumber});
        }
        [HttpPost]
        [Route("add")]
        public AccountingBookViewModel Create([FromBody] AccountingBookViewModel model)
        {
            return accountingBookMap.Create(model);
        }
        [HttpDelete]
        [Route("delete")]
        public bool Delete([FromBody] AccountingBookViewModel model)
        {
            return accountingBookMap.Delete(model);
        }
        [HttpPut]
        [Route("edit")]
        public bool Edit([FromBody] AccountingBookViewModel model)
        {
            return accountingBookMap.Update(model);
        }
    }
}
