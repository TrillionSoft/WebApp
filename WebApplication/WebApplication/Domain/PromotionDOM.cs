using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Domain
{
    public class PromotionDOM
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public double Unit_Price { get; set; }
        public double Offer_Price { get; set; }
        public double Offer_Percentage { get; set; }
        public string Offer_Until_Date { get; set; }
        public double Total_Price { get; set; }
        public byte[] Image { get; set; }
    }
}