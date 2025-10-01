using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EComApp.Repositories
{
    public class CartRepository : ICartRepository
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

        public async Task<int> AddItem(int ArticleId, int qty)
        {
            string userId = GetUserId();
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new Exception("user is not logged-in");
                }

                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart()
                    {
                        UserId = userId
                    };
                    _context.ShoppingCarts.Add(cart);
                    _context.SaveChanges();
                }


                var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.ShoppingCartId == cart.Id && cd.ArticleId == ArticleId);

                if (cartDetail != null)
                {
                    cartDetail.Quantity += qty;
                    _context.CartDetails.Update(cartDetail);
                }
                else
                {
                    cartDetail = new CartDetail()
                    {
                        ArticleId = ArticleId,
                        Quantity = qty,
                        ShoppingCartId = cart.Id
                    };
                    _context.CartDetails.Add(cartDetail);
                }
                _context.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
            }
            var count = await GetCartItemCount(userId);
            return count;
        }
        public async Task<int> RemoveItem(int ArticleId)
        {
            string userId = GetUserId();
            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new Exception("user is not logged-in");

                var cart = await GetCart(userId);
                if (cart == null)
                    throw new Exception("Cart is empty");

                var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.ShoppingCartId == cart.Id && cd.ArticleId == ArticleId);
                if(cartDetail == null)
                    throw new Exception("Item not found in cart");
                else if(cartDetail.Quantity == 1)
                {
                    _context.CartDetails.Remove(cartDetail);
                }
                else
                {
                    cartDetail.Quantity -= 1;
                    _context.CartDetails.Update(cartDetail);
                }
                _context.SaveChanges();
                //await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
            }
            var count = await GetCartItemCount(userId);
            return count;
        }
        public async Task<IEnumerable<ShoppingCart>> GetUserCart()
        {
            string userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");

            var shoppingCarts = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Article)
                .ThenInclude(a => a.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();
            return shoppingCarts;
;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var result = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId);
            return result;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in _context.ShoppingCarts
                              join cartDetail in _context.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select cartDetail).ToListAsync();
            return data.Count;
        }

        public string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(user);
            return userId;
        }

    }
}
