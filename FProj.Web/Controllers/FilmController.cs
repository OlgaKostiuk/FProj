using FProj.Api;
using FProj.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

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
        public ActionResult UploadPoster(int Id, HttpPostedFileBase file, bool IsPoster = true)
        {
            string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string localPath = Path.Combine(Server.MapPath($"~{WebConfigurationManager.AppSettings["ImageFolder"]}"), uniqueName);

            file.SaveAs(localPath);
            var path = UnitOfWork.Instance.ImageRepository.AddPoster(new Api.ImageApi() { Path = uniqueName }, Id);
            return Json(new { Ok = true, Path = WebConfigurationManager.AppSettings["ImageFolder"] + path });
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            ViewBag.Genres = UnitOfWork.Instance.GenreRepository.GetAll();
            return View(UnitOfWork.Instance.FilmRepository.Default());
        } 

        [HttpPost]
        public ActionResult Create(FilmApi model, int[] Genres, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> frames)
        {
            model.DateCreated = DateTime.Now;
            var film = UnitOfWork.Instance.FilmRepository.Create(model);

            //model.Genres.Count();
            foreach(var item in Genres)
            {
                UnitOfWork.Instance.FilmRepository.AddGenre(film, item);
            }
           
            if (file != null)
                UploadPoster(film.Id, file);

            if (frames.FirstOrDefault() != null)
            {
                var filesApi = new List<ImageApi>();
                foreach (var frame in frames)
                {
                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(frame.FileName);
                    string localPath = Path.Combine(Server.MapPath($"~{WebConfigurationManager.AppSettings["ImageFolder"]}"), uniqueName);

                    frame.SaveAs(localPath);
                    filesApi.Add(new ImageApi() { Path = uniqueName });
                }
                UnitOfWork.Instance.ImageRepository.AddPictures(filesApi, film.Id);
            }

            return RedirectToAction("Details", new { Id = film.Id });
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit";
            ViewBag.Genres = UnitOfWork.Instance.GenreRepository.GetAll();
            return View("Create", UnitOfWork.Instance.FilmRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult Edit(FilmApi model, int[] Genres, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> frames)
        {

            var film = UnitOfWork.Instance.FilmRepository.Update(model);

            //model.Genres.Count();
            UnitOfWork.Instance.FilmRepository.RemoveGenres(film);
            foreach (var item in Genres)
            {
                UnitOfWork.Instance.FilmRepository.AddGenre(film, item);
            }

            if (file != null)
                UploadPoster(film.Id, file);

            if (frames.FirstOrDefault() != null)
            {
                var filesApi = new List<ImageApi>();
                foreach (var frame in frames)
                {
                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(frame.FileName);
                    string localPath = Path.Combine(Server.MapPath($"~{WebConfigurationManager.AppSettings["ImageFolder"]}"), uniqueName);

                    frame.SaveAs(localPath);
                    filesApi.Add(new ImageApi() { Path = uniqueName });
                }
                UnitOfWork.Instance.ImageRepository.AddPictures(filesApi, film.Id);
            }

            return RedirectToAction("Details", new { Id = film.Id });
        }
        
    }
}