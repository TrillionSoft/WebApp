using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Domain
{
    public class SubProductCategory
    {
        public List<int> ID { get; set; }
        public List<int> PC_ID { get; set; }
        public List<string> Type { get; set; }
        public List<string> Description { get; set; }
        public List<bool> Deleted { get; set; }
    }
}