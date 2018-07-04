using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        UserDOM user = new UserDOM();
        UserDA userDA = new UserDA();

        // GET: Home
        public ActionResult Index()
        {
            user = userDA.selectUserPassword("CJ");
            ViewBag.Message = user.Password;
            return View();
        }
    }
}