using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models
{
    public class UserComment
    {
        public int ProductId { get; set; }
        public string BuyerId { get; set; }
        public byte? Rate { get; set; }
        public string Comment { get; set; }
        [Key]
        public int Id { get; set; }
        [ForeignKey("BuyerId")]
        public virtual AppUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
