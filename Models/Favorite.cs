using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class Favorite
    {
        public int ProductId { get; set; }
        public string BuyerId { get; set; }
        [Key]
        public int Id { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("BuyerId")]
        public virtual AppUser User { get; set; }
    }
}
