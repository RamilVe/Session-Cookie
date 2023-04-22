using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.ViewModels;

namespace Pustok.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;

        public BookController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult GetBookModal(int id)
        {
            var book = _context.Books
                .Include(x => x.Genre)
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .FirstOrDefault(x => x.Id == id);

            return PartialView("_BookModalPartial", book);
        }

        public IActionResult AddToBasket(int id)
        {
            List<BasketCookieItemViewModel> basketItems;
            var basket = HttpContext.Request.Cookies["basket"];

            if (basket == null)
                basketItems = new List<BasketCookieItemViewModel>();
            else
                basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemViewModel>>(basket);

            var wantedBook = basketItems.FirstOrDefault(x => x.BookId == id);

            if (wantedBook == null)
                basketItems.Add(new BasketCookieItemViewModel { Count = 1, BookId = id });
            else
                wantedBook.Count++;


            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));

            BasketViewModel basketVM = new BasketViewModel();
            foreach (var item in basketItems)
            {
                var book = _context.Books.Include(x => x.BookImages.Where(x => x.PosterStatus == true)).FirstOrDefault(x => x.Id == item.BookId);


                basketVM.BasketItems.Add(new BasketItemViewModel
                {
                    Book = book,
                    Count = item.Count
                });

                var price = book.DiscountPercent > 0 ? (book.SalePrice * (100 - book.DiscountPercent) / 100) : book.SalePrice;
                basketVM.TotalPrice += (price * item.Count);
            }

            return PartialView("_BasketCartPartial", basketVM);
        }

        public IActionResult ShowBasket()
        {
            var basket = HttpContext.Request.Cookies["basket"];
            var basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemViewModel>>(basket);

            return Json(basketItems);
        }
        public IActionResult Detail(int id)
        {

            var book = _context.Books.
                Include(x => x.Genre).
                Include(x => x.Author).
                Include(x => x.BookImages).
                FirstOrDefault(x => x.Id == id);
            return View(book);
        }
    }
}
