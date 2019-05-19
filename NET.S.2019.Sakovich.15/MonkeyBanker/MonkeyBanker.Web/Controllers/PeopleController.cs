using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Entities;
using MonkeyBanker.Data;
using MonkeyBanker.Web.Models;

namespace MonkeyBanker.Web.Controllers
{
    public class PeopleController : CrudController<Person>
    {
        public PeopleController(ICrudable<Person> crudToPeople)
            : base(crudToPeople)
        {
        }
    }
}