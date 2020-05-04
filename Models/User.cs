using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
    }
}
