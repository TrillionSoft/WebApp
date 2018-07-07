using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DA;
using WebApplication.Domain;
using WebApplication.Lib;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PromotionController : Controller
    {

        DBEntities db = new DBEntities();
        ProductDOM product = new ProductDOM();
        PromotionDOM PromotionProduct = new PromotionDOM();
        ProductDA productDA = new ProductDA();
        PromotionDA promotionDA = new PromotionDA();
        LIB lib = new LIB();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPromotionProduct()
        {
            // product = productDA.selectProduct(2);
            //  product.Material = "01/08/2018";
            // return View(product);
            return View();
        }

        [HttpPost]
        public void AddPromotionProduct(PromotionPostDOM promotion)
        {
            if (promotion.Image != null)
            {
                foreach (var file in promotion.Image)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        Bitmap bmp = new Bitmap(file.InputStream);
                        Size size = new Size();
                        size.Width = 400;
                        size.Height = 500;
                        Bitmap resizeImage = ResizeImage(bmp, size);
                        ImageConverter converter = new ImageConverter();
                        byte[] bimage = (byte[])converter.ConvertTo(resizeImage, typeof(byte[]));

                        PromotionProduct.Name = promotion.Name; 
                        PromotionProduct.Brand = promotion.Brand;
                        PromotionProduct.Unit_Price = 40.00;  //DUMMY
                        PromotionProduct.Offer_Price = 4.00; //DUMMY
                        PromotionProduct.Offer_Percentage = 10; //DUMMY
                        PromotionProduct.Offer_Until_Date = "08/01/2018"; //DUMMY
                        PromotionProduct.Total_Price = 36.00; //DUMMY
                        PromotionProduct.Image = bimage; 
                        int result = promotionDA.InsertProduct(PromotionProduct);
                    }
                    
                }
            }       
        }

        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }
    }
}