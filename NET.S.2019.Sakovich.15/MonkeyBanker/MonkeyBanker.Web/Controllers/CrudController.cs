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
    public class CrudController<T> : Controller
        where T : IIdentifiable<int>, new()
    {
        protected ICrudable<T> crud;

        public CrudController(ICrudable<T> crud)
        {
            this.crud = crud;
        }

        public ActionResult Index()
        {
            CrudIndexViewModel<T> model = new CrudIndexViewModel<T>
            {
                Entitites = this.crud.Read()
            };

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            T retrievedEntity = this.crud.Read(id);

            if (retrievedEntity != null)
            {
                return View(retrievedEntity);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(T editedEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editedEntity);
            }

            if (this.crud.Update(editedEntity) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                // Handling concurrency problems in future
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            T newEntity = new T();

            return this.View(newEntity);
        }

        [HttpPost]
        public ActionResult Create(T newEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(newEntity);
            }

            if (this.crud.Create(newEntity) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                // Handling concurrency problems in future
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            T delEntity = this.crud.Read(id);

            if (delEntity == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(delEntity);
        }

        [HttpPost]
        public ActionResult Delete(T delEntity)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(delEntity);
            }

            this.crud.Delete(delEntity.ID);

            return this.RedirectToAction("Index");
        }
    }
}