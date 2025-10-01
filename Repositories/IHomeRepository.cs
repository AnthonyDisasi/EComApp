namespace EComApp
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Article>> GetArticles(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Category();
    }
}