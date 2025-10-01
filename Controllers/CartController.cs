using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EComApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult AddItem(int articleId, int qty = 1)
        {
            return View();
        }
        public IActionResult RemoveItem(int articleId)
        {
            return View();
        }
        public IActionResult GetUserCart()
        {
            return View();
        }
        public IActionResult GetTotalItemInCart()
        {
            return View();
        }
    }
}
