using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class Product
    {
        public Product()
        {
            Favorite = new HashSet<Favorite>();
            OrderProduct = new HashSet<OrderProduct>();
            UserComment = new HashSet<UserComment>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public byte? Rate { get; set; }
        public string SellerId { get; set; }
        [ForeignKey("SellerId")]
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual ICollection<UserComment> UserComment { get; set; }
    }
}
