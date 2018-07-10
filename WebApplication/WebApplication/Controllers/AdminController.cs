using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var Category = db.Product_Category;
            return View(Category);
        }

        public ActionResult Home()
        {
            return View();
        }

        
        public ActionResult MaintainCategory(string type)
        {
           var Category = db.Product_Category;           
            if (Request.IsAjaxRequest())
            {
                var Specific_Category = db.Product_Category.Where(o => o.Type.Contains(type));
                System.Threading.Thread.Sleep(1000); // Sleep 1 second
                return PartialView("_SearchCategory", Specific_Category); // Return partial
            }
            return View(Category); // Return entire
        }
    }
}