using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Collection.BLL.DTO;
using Collection.BLL.Interfaces;
using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using Collection.BLL.Profiles;
using AutoMapper;
using Collection.DAL.Repositories;

namespace Collection.BLL.Services
{
    public class CollectionService : ICollectionService
    {
        private IUnitOfWork UnitOfWork { get; }
        public IConfiguration Configuration { get; }

        private IAccountService _accountService;
        private readonly IMapper mapper;

        public CollectionService(IUnitOfWork unitOfWork, IConfiguration configuration, IAccountService accountService)
        {
            UnitOfWork = unitOfWork;
            Configuration = configuration;
            _accountService = accountService;
        }

        public IEnumerable<string> GetThemes()
        {
            var themes = new List<string>();
            var props = typeof(CollectionModel).GetProperties();

            foreach (var prop in props)
            {
                var attrs = new List<string>() { "Movies", "Books" };
                foreach (var attr in attrs)
                {
                    if (attr != null)
                    {
                        themes.Add(attr);
                    }
                }
            }
            return themes;
        }

        public async Task CreateCollection(ClaimsPrincipal user, CollectionDTO collectionDto, string userId = "")
        {
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                collectionDto.User = await _accountService.GetCurrentUser(user, userId);
                var collection = UnitOfWork.Collections.Add(mapper.Map<CollectionDTO, CollectionModel>(collectionDto));
                UnitOfWork.Collections.Add(collection);
                await UnitOfWork.SaveAsync();
                await UploadImages(collection, collectionDto.Files);
                await transaction.CommitAsync();
            }
        }

        public async Task<CollectionDTO> GetCollection(int collectionId)
        {
            var collection = await UnitOfWork.Collections.Get(collectionId);

            var collectionDto = mapper.Map<CollectionModel, CollectionDTO>(collection);
            collectionDto.Topics = GetThemes();
            return collectionDto;
        }

        public IEnumerable<string> GetImages(int collectionId)
        {
            return UnitOfWork.Images
                .Find(image => image.CollectionId == collectionId)
                .Select(image => image.ImagePath)
                .ToList();
        }

        public async Task EditCollection(ClaimsPrincipal claimsPrincipal, CollectionDTO collectionDto)
        {
            var collection = await UnitOfWork.Collections.Get((int)collectionDto.Id, collection => collection.User);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                collectionDto.User = collection.User;
                mapper.Map<CollectionDTO, CollectionModel>(collectionDto, collection);
                UnitOfWork.Collections.Update(collection);
                await UploadImages(collection, collectionDto.Files);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task DeleteCollection(ClaimsPrincipal claimsPrincipal, int collectionId)
        {
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var collection = await UnitOfWork.Collections.Get(collectionId, collection => collection.User);
                await UploadImages(collection, new List<IFormFile>());
                await UnitOfWork.Collections.Delete(collectionId);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
        }

        public IEnumerable<CollectionModel> GetLagestNumberItems()
        {
            return UnitOfWork.Context.CollectionModels
                .IncludeMultiple(
                collection => collection.Images,
                collection => collection.User)
                .OrderByDescending(collection => collection.Items.Count())
                .Take(6);
        }

        public IEnumerable<CollectionModel> GetAllCollections(int page = 1)
        {
            throw new NotImplementedException();
        }

        private async Task DeleteImages(Cloudinary cloudinary, CollectionModel collection)
        {
            var images = UnitOfWork.Images.Find(image => image.CollectionId == collection.Id).ToList();
            foreach (var image in images)
            {
                var deletionParams = new DeletionParams(image.PublicId);
                cloudinary.Destroy(deletionParams);
                await UnitOfWork.Images.Delete(image.Id);
            }
            await UnitOfWork.SaveAsync();
        }

        private async Task UploadImages(CollectionModel collection, List<IFormFile> files)
        {
            var account = new Account("dqkvffnpu", "132528913365738", "DbiEgxXbyI60ZcZwIEh4yS5Bu2Y");
            var cloudinary = new Cloudinary(account);
            await DeleteImages(cloudinary, collection);
            foreach (var file in files)
            {
                var uploadResult = cloudinary.Upload(new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                });
                UnitOfWork.Images.Add(new Image
                {
                    ImagePath = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId,
                    Collection = collection
                });
                await UnitOfWork.SaveAsync();
            }
        }
    }
}