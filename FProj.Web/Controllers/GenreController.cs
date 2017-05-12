using FProj.Api;
using FProj.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FProj.Web.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View(UnitOfWork.Instance.GenreRepository.Default());
        }

        [HttpPost]
        public ActionResult Create(GenreApi model)
        {
            return Json(UnitOfWork.Instance.GenreRepository.Create(model));
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit";
            return View("Create", UnitOfWork.Instance.GenreRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult Edit(GenreApi model)
        {
            return Json(UnitOfWork.Instance.GenreRepository.Update(model));
        }
    }
}