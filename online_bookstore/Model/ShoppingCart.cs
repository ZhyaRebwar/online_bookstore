using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace online_bookstore.Model
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to ApplicationUser
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // Navigation property to ShoppingCartItems
        public virtual ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}