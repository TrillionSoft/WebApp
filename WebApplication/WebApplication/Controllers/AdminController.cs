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

        public ActionResult EditCategory(int id)
        {
            CategoryModel category = new CategoryModel();
            category.Main_Category = db.Product_Category.Find(id);
            category.Sub_Category = db.Sub_Product_Category.Where(e => e.PC_ID == id).ToList();

            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryModel category)
        {
            var mainCategory = db.Product_Category.Find(category.Main_Category.ID);
            mainCategory.Type = category.Main_Category.Type;
            mainCategory.Description = category.Main_Category.Description;


            db.SaveChanges();

            return RedirectToAction("MaintainProduct");
        }
    }
}