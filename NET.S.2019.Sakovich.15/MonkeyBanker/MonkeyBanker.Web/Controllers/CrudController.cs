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
    public class CrudController<TEntity> : Controller
        where TEntity : IIdentifiable<int>, new()
    {
        protected ICrudable<TEntity> crud;

        public CrudController(ICrudable<TEntity> crud)
        {
            this.crud = crud;
        }

        public ActionResult Index()
        {
            CrudIndexViewModel<TEntity> model = this.LoadIndexViewModel();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            CrudFormViewModel<TEntity> model = this.LoadFormViewModel(id);

            this.InitializeFormViewModelOnEdit(model);

            if (model.Entity != null)
            {
                return View(model);
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(CrudFormViewModel<TEntity> model)
        {
            if (!this.ModelState.IsValid)
            {
                this.InitializeFormViewModelOnEdit(model);

                return this.View(model);
            }

            if (this.crud.Update(model.Entity) == 1)
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
            CrudFormViewModel<TEntity> model = this.LoadFormViewModel();

            this.InitializeFormViewModelOnCreate(model);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(CrudFormViewModel<TEntity> model)
        {
            if (!this.ModelState.IsValid)
            {
                this.InitializeFormViewModelOnCreate(model);

                return this.View(model);
            }

            if (this.crud.Create(model.Entity) == 1)
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
            CrudDetailsViewModel<TEntity> model = this.LoadDetailsViewModel(id);

            if (model.Entity == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, object post)
        {
            this.crud.Delete(id);

            return this.RedirectToAction("Index");
        }

        protected virtual CrudIndexViewModel<TEntity> LoadIndexViewModel()
        {
            return new CrudIndexViewModel<TEntity>
            {
                Entitites = this.crud.Read()
            };
        }

        protected virtual CrudFormViewModel<TEntity> LoadFormViewModel()
        {
            return new CrudFormViewModel<TEntity>
            {
                Entity = new TEntity()
            };
        }

        protected virtual CrudFormViewModel<TEntity> LoadFormViewModel(int id)
        {
            return new CrudFormViewModel<TEntity>
            {
                Entity = this.crud.Read(id)
            };
        }

        protected virtual void InitializeFormViewModelOnCreate(CrudFormViewModel<TEntity> model)
        {
            model.ButtonLabel = "Create";
        }

        protected virtual void InitializeFormViewModelOnEdit(CrudFormViewModel<TEntity> model)
        {
            model.ButtonLabel = "Apply";
        }

        protected virtual CrudDetailsViewModel<TEntity> LoadDetailsViewModel(int id)
        {
            return new CrudDetailsViewModel<TEntity>
            {
                Entity = this.crud.Read(id)
            };
        }
    }
}