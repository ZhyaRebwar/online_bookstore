using Microsoft.AspNetCore.Identity;

namespace online_bookstore.Model
{
    public class ApplicationUser : IdentityUser
    {
        // Add additional properties here if needed
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCart> ShoppingCart { get; set; }
        
    }
}