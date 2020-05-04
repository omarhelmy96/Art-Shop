using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Products = new HashSet<Product>();
            UserComment = new HashSet<UserComment>();
            Favorite = new HashSet<Favorite>();
            Order = new HashSet<Order>();
        }
        public int USerId { get; set; }
        [ForeignKey("USerId")]
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserComment> UserComment { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Favorite> Favorite { get; set; }
    }
}
