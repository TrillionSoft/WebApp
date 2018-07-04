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
        ProductCategoryDOM product = new ProductCategoryDOM();
        UserDA userDA = new UserDA();
        ProductCategoryDA productDA = new ProductCategoryDA();

        // GET: Home
        public ActionResult Index()
        {
            //user = userDA.selectUserPassword("CJ");
            //  product = productDA.selectProductCategory();
            product = productDA.selectALLCategory();
            ViewBag.Message = user.Password;

            return View(product);
        }

        public ActionResult Menu()
        {
            product = productDA.selectALLCategory();
            return PartialView(product);
        }
    }
}