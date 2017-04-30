using FProj.Api;
using System.Web.Mvc;
using FProj.Repository;

namespace FProj.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserApi model, string password)
        {
            return Json(UnitOfWork.Instance.UserRepository.Add(model, password));
        }
    }
}