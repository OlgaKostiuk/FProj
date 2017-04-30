using FProj.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FProj.Api;

namespace FProj.Web.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        public ActionResult Index()
        {
            var data = UnitOfWork.Instance.FilmRepository.GetPage(1);

            return View(data);
        }

        [HttpGet]
        public ActionResult GetPage(PageRequest request)
        {
            var data = request == null ? UnitOfWork.Instance.FilmRepository.GetPage(1) : UnitOfWork.Instance.FilmRepository.GetPage(request.PageNumber, request.CountPerPage);

            return PartialView("FilmList", data.Data);
        }

        public ActionResult Details(int Id) => View(UnitOfWork.Instance.FilmRepository.GetById(Id));

        [HttpPost]
        public ActionResult UploadPoster(int Id, HttpPostedFileBase file) {
            string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string localPath = Path.Combine(Server.MapPath($"~{WebConfigurationManager.AppSettings["ImageFolder"]}"), uniqueName);
            
            file.SaveAs(localPath);
            var path = UnitOfWork.Instance.ImageRepository.AddPoster(new Api.ImageApi() { Path = uniqueName }, Id);
            return Json(new { Ok = true, Path = WebConfigurationManager.AppSettings["ImageFolder"] + path });
        }

        public ActionResult Create() => View(UnitOfWork.Instance.FilmRepository.Default());

        [HttpPost]
        public ActionResult Create(FilmApi model, HttpPostedFileBase file)
        {
            model.DateCreated = DateTime.Now;
            var film = UnitOfWork.Instance.FilmRepository.Create(model);
            if (file != null)
                UploadPoster(film.Id, file);

            return RedirectToAction("Details", new { Id = film.Id });
        }
    }
}