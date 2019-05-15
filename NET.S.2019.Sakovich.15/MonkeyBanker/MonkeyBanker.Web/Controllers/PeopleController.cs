using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Entities;
using MonkeyBanker.Data;
using MonkeyBanker.Web.Models.People;

namespace MonkeyBanker.Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ICrudable<Person> CrudToPeople;

        public PeopleController(ICrudable<Person> crudToPeople)
        {
            this.CrudToPeople = crudToPeople;
        }

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                People = this.CrudToPeople.Read()
            };

            return View(model);
        }
    }
}