using AutoMapper;
using Collection.BLL.DTO;
using Collection.BLL.Interfaces;
using Collection.DAL.Interfaces;
using Collection.DAL.Repositories;
using Collection.ViewModels;
using MarkdownSharp;
using Microsoft.AspNetCore.Mvc;

namespace Collection.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private IUnitOfWork UnitOfWork;

        private readonly IMapper mapper;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        //!!!!!!!!!!!!!!!!!!!!!
        [Route("/", Name = "Profile")]
        public async Task<IActionResult> Index(
            [FromQuery] int page = 1,
            [FromQuery] string userId = "")
        {
            var collections = _collectionService.GetAllCollections(page);
            var markdown = new Markdown();
            foreach (var entity in collections)
            {
                entity.ShortDescription = markdown.Transform(entity.ShortDescription);
            }
            ViewData["userId"] = userId;
            Response.Cookies.Append("collectionPage", page.ToString());
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string userId = "")
        {
            Response.Cookies.Delete("collectionId");
            ViewData["userId"] = userId;
            return View("Form", new CollectionViewModel
            {
                Themes = _collectionService.GetThemes()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionViewModel model, [FromQuery] string userId = "")
        {
            await _collectionService.CreateCollection(
                User,
                mapper.Map<CollectionViewModel, CollectionDTO>(model),
                userId);
            return RedirectToAction("Index", new { userId = userId });
        }

        [HttpGet("/Edit")]
        public async Task<IActionResult> EditCollection(
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                var collectionDto = await _collectionService.GetCollection(collectionId);
                Response.Cookies.Append("collectionId", collectionId.ToString());
                var collection = mapper.Map<CollectionDTO, CollectionViewModel>(collectionDto);
                var collection1 = await UnitOfWork.Collections.Get(collectionId, collection => collection.User);

                ViewData["userId"] = userId;
                return View("Form", collection);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("/{collectionId}/Images")]
        public IEnumerable<string> GetImages(int collectionId)
        {
            return _collectionService.GetImages(collectionId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            CollectionViewModel model,
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                model.Id = collectionId;
                await _collectionService.EditCollection(
                    User,
                    mapper.Map<CollectionViewModel, CollectionDTO>(model));
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(
            [FromQuery(Name = "collectionId")] int collectionId,
            [FromQuery] string userId = "")
        {
            try
            {
                await _collectionService.DeleteCollection(User, collectionId);
                return RedirectToAction("Index", new { userId = userId });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}