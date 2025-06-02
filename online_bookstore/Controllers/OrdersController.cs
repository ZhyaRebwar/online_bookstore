using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online_bookstore.Data;
using online_bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_bookstore.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound(new { message = "No orders found." });
            }

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(string userId, string shippingAddress)
        {
            // 1. Get the shopping cart and include its items and books
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                return BadRequest("Shopping cart is empty or not found.");
            }

            // 2. Calculate total
            decimal total = cart.Items.Sum(i => i.Book.Price * i.Quantity);

            // 3. Create order
            var order = new Order
            {
                UserId = int.Parse(userId), // Adjust this if your UserId is a string in ApplicationUser
                ShippingAddress = shippingAddress,
                Status = OrderStatus.Pending,
                TotalAmount = total,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cart.Items)
            {
                // Optional: Check book stock
                if (item.Quantity > item.Book.StockQuantity)
                {
                    return BadRequest($"Not enough stock for book '{item.Book.Title}'.");
                }

                // Deduct stock
                item.Book.StockQuantity -= item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    PriceAtOrder = item.Book.Price
                });
            }

            // Save order
            _context.Orders.Add(order);

            // Clear shopping cart
            _context.ShoppingCartItems.RemoveRange(cart.Items);
            _context.ShoppingCarts.Remove(cart); // Optional: keep cart if you prefer

            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully!", orderId = order.Id });
        }


        // DELETE: api/Orders/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Order with ID {order.Id} was deleted successfully." });
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
