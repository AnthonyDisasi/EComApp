namespace EComApp.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int ArticleId, int qty);
        Task<int> RemoveItem(int ArticleId);
        Task<IEnumerable<ShoppingCart>> GetUserCart();
        Task<int> GetCartItemCount(string userId);
        Task<ShoppingCart> GetCart(string userId);
    }
}
