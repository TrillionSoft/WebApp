using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;
using WebApplication.Lib;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        UserDOM user = new UserDOM();
        ProductCategoryDOM product = new ProductCategoryDOM();
        UserDA userDA = new UserDA();
        ProductCategoryDA productDA = new ProductCategoryDA();

         LIB lib = new LIB();

        // GET: Home
        public ActionResult Index()
        {
            //user = userDA.selectUserPassword("CJ");
            //  product = productDA.selectProductCategory();
            //product = productDA.selectALLCategory();

            //List<int> ID = new List<int>();
            //List<string> Type = new List<string>();
            //List<string> Description = new List<string>();
            //List<bool> Deleted = new List<bool>();

            //ID.Add(8);
            //Type.Add("Alibab3");
            //Description.Add("");
            //Deleted.Add(true);

            //product.ID = ID;
            //product.Type = Type;
            //product.Description = Description;
            //product.Deleted = Deleted;
            //int result = productDA.updateProductCategory(product);

            user.LoginID = "Testing123";
            user.LoginPassword = "Testing123";
            if (LIB.insertFunction(user,"[User]") > 0)
            {
                ViewBag.Message = "Successful Insert";
            }
            return View();
        }

        public ActionResult Menu()
        {
            product = productDA.selectALLCategory();
            return PartialView(product);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}