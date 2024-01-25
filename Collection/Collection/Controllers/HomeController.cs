using Collection.BLL.DTO;
using Collection.BLL.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Collection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICollectionService _collectionService;

        public HomeController(IItemService itemService, ICollectionService collectionService)
        {
            _itemService = itemService;
            _collectionService = collectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllCollections(int page = 1)
        {
            Response.Cookies.Append("collectionPage", page.ToString());
            ViewData["action"] = "GetAllCollections";
            ViewData["controller"] = "Home";
            return View("../Collection/Index", _collectionService.GetAllCollections(page));
        }

        public IEnumerable<TagDTO> GetTagsCloud()
        {
            return _itemService.GetTagsCloud();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl);
        }
    }
}