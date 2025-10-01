using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace EComApp.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Category>> Category()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticles(string sTerm = "", int categoryId = 0)
        {
            sTerm = sTerm.Trim().ToLower();
            IEnumerable<Article> articles = await (from article in _db.Articles
                                                   join category in _db.Categories
                                                   on article.CategoryId equals category.Id
                                                   where string.IsNullOrWhiteSpace(sTerm) || (article != null && article.ArticleName.ToLower().StartsWith(sTerm))
                                                   where categoryId == 0 || article.CategoryId == categoryId
                                                   select new Article
                                                   {
                                                       Id = article.Id,
                                                       ArticleName = article.ArticleName,
                                                       Description = article.Description,
                                                       Price = article.Price,
                                                       Image = article.Image,
                                                       CategoryName = category.CategoryName
                                                   }).ToListAsync();
            if (categoryId > 0)
            {
                articles = articles.Where(a => a.CategoryId == categoryId).ToList();

            }
            return articles;
        }
    }
}
