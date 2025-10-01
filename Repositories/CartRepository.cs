using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EComApp.Repositories
{
    public class CartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public CartRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> AddItem(int ArticleId, int qty)
        {
            string userId = GetUserId();
            if(string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            var cart = await GetCart(userId);
            if(cart == null)
            {
                cart = new ShoppingCart()
                {
                    UserId = userId
                };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();
            }
        }

        private async Task<ShoppingCart> GetCart(string userId)
        {
            var result = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId);
            return result;
        }

        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);
            return userId;
        }
    }
}
