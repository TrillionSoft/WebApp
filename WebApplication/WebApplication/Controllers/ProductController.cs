using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;
using WebApplication.Lib;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        ProductDOM product = new ProductDOM();
        ProductDA productDA = new ProductDA();
        LIB lib = new LIB();
        Image File;
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            product = productDA.selectProduct(2);
            return View(product);
        }

        [HttpPost]
        public void AddProduct(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        //var fileName = Path.GetFileName(file.FileName);
                        //var path = Path.Combine(Server.MapPath("~/Upload"), fileName);
                        //file.SaveAs(path);  //this will save to my folder
                        //// File = Image.FromFile(fileName);
                        Bitmap bmp = new Bitmap(file.InputStream);
                        ImageConverter converter = new ImageConverter();
                        byte[] bimage = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                        product.PC_ID = 0;
                        product.SPC_ID = 0;
                        product.Brand = "";
                        product.Description = "";
                        product.Production_Country = "";
                        product.Material = "";
                        product.Deleted = false;
                        product.Image = bimage;

                        int result = productDA.InsertProduct(product);
                    }
                }
            }
        }
    }
}