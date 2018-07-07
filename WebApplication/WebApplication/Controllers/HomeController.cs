using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;
using WebApplication.Lib;
using System.Web.Security;
using System.Security.Principal;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();
        UserDOM user = new UserDOM();
        ProductCategoryDOM product = new ProductCategoryDOM();
        UserDA userDA = new UserDA();
        ProductCategoryDA productDA = new ProductCategoryDA();

         LIB lib = new LIB();

        // GET: Home
        [Authorize]
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
            user.LoginPassword = "Testing3";
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

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        public ActionResult Login()
        {
            EnsureLoggedOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDOM input)
        {

            if (ModelState.IsValid)
            {
                var login = userDA.checkLogin(input.LoginID, input.LoginPassword);
                if (login == true)
                {
                    FormsAuthentication.SetAuthCookie(input.LoginID, input.RememberMe);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Promotion()
        {
            var product = db.Promotions;
            return View(product);
        }
    }
}