using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Domain
{
    public class ProductDOM
    {
        public int ID { get; set; }
        public int PC_ID { get; set; }
        public int SPC_ID { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Production_Country { get; set; }
        public string Material { get; set; }
        public bool Deleted { get; set; }
        public Byte[] Image { get; set; }
    }
}