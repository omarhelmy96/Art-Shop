using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class Order
    {
        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();

        }
        public int Id { get; set; }
        public string BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual AppUser AppUser { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Cost { get; set; }
        public bool? Status { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
