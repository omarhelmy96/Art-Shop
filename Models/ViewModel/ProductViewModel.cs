using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models.ViewModel
{
    public class ProductViewModel
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public List<IFormFile> Photos { get; set; }
        
    }
}
