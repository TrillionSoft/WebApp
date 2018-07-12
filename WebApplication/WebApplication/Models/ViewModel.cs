using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CategoryModel
    {
        public Product_Category Main_Category {get;set;}
        public List<Sub_Product_Category> Sub_Category { get; set; }

    }
}