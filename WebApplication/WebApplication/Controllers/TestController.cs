using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        DBEntities db = new DBEntities();
        UserDOM user = new UserDOM();
        ProductCategoryDOM product = new ProductCategoryDOM();
        UserDA userDA = new UserDA();
        ProductCategoryDA productDA = new ProductCategoryDA();

        // GET: Test
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDOM input)
        {
            var user = new User
            {
                LoginID = input.LoginID,
                LoginPassword = input.LoginPassword
            };

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Retrive()
        {
            var user = db.Users;
            return View(user);
        }

        public ActionResult RetriveDetails(int id)
        {
            var user = db.Users.Find(id);

            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int id, UserDOM input)
        {
            var user = db.Users.Find(id);

            user.LoginID = input.LoginID;
            user.LoginPassword = input.LoginPassword;

            db.SaveChanges();
            return RedirectToAction("Retrive");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}