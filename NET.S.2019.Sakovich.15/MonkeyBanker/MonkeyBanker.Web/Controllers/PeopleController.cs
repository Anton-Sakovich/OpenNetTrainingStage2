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

        public ActionResult Edit(int id)
        {
            Person retrievedPerson = this.CrudToPeople.Read(id);

            if (retrievedPerson != null)
            {
                return View(retrievedPerson);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Person editedPerson)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editedPerson);
            }

            this.CrudToPeople.Update(editedPerson);

            return this.RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            Person newPerson = new Person()
            {
                ID = 0
            };

            return this.View(newPerson);
        }

        [HttpPost]
        public ActionResult Create(Person newPerson)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(newPerson);
            }

            if (this.CrudToPeople.Create(newPerson) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Person delPerson = this.CrudToPeople.Read(id);

            if (delPerson == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(delPerson);
        }

        [HttpPost]
        public ActionResult Delete(Person delPerson)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(delPerson);
            }

            this.CrudToPeople.Delete(delPerson.ID);

            return this.RedirectToAction("Index");
        }
    }
}