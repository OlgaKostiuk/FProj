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
        [Authorize(Users="admin@gmail.com")]
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View(UnitOfWork.Instance.GenreRepository.Default());
        }

        [Authorize(Users = "admin@gmail.com")]
        [HttpPost]
        public ActionResult Create(GenreApi model)
        {
            return Json(UnitOfWork.Instance.GenreRepository.Create(model));
        }

        [Authorize(Users = "admin@gmail.com")]
        public ActionResult Edit(int Id)
        {
            GenreApi genre = UnitOfWork.Instance.GenreRepository.GetById(Id);
            if (genre == null)
                return RedirectToAction("Create");
            ViewBag.Title = "Edit";
            return View("Create", genre);
        }

        [Authorize(Users = "admin@gmail.com")]
        [HttpPost]
        public ActionResult Edit(GenreApi model)
        {
            return Json(UnitOfWork.Instance.GenreRepository.Update(model));
        }
    }
}