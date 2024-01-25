using AutoMapper;
using Collection.BLL.DTO;
using Collection.BLL.Interfaces;
using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;

namespace Collection.BLL.Services
{
    public class ItemService : IItemService
    {
        public IUnitOfWork UnitOfWork { get; }

        private IAccountService _accountService;

        private ICollectionService _collectionService;
        private readonly IMapper mapper;

        public ItemService(
            IUnitOfWork unitOfWork,
            IAccountService accountService,
            ICollectionService collectionService)
        {
            UnitOfWork = unitOfWork;
            _accountService = accountService;
            _collectionService = collectionService;
        }

        private IEnumerable<TagBLLModel> DeserializeTags(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<TagBLLModel>>(json);
        }

        private async Task AddTags(Item model, IEnumerable<TagBLLModel> tags)
        {
            foreach (var newTag in tags)
            {
                var existingTags = UnitOfWork.Tags.Find(tag => tag.Title == newTag.Value).ToList();
                if (existingTags.Count() == 0)
                {
                    model.Tags.Add(new Tag() { Title = newTag.Value });
                }
                else
                {
                    model.Tags.Add(existingTags.First());
                }
            }
            await UnitOfWork.SaveAsync();
        }

        public async Task CreateItem(
            ClaimsPrincipal userPrincipal,
            ItemDTO itemDto,
            string userId = "")
        {
            var collection = UnitOfWork.Collections.Get((int)itemDto.Id, v => v.User);

            var tags = DeserializeTags(itemDto.TagsJson);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var model = mapper.Map<ItemDTO, Item>(itemDto);
                UnitOfWork.Items.Add(model);
                await UnitOfWork.SaveAsync();
                await AddTags(model, tags);
                await transaction.CommitAsync();
            }
        }

        public async Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDTO itemDto)
        {
            var collection = UnitOfWork.Collections.Get((int)itemDto.CollectionId, v => v.User);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var model = mapper.Map<ItemDTO, Item>(itemDto);
                UnitOfWork.Items.Update(model);
                await UnitOfWork.SaveAsync();
                model = await UnitOfWork.Items.Get(model.Id, item => item.Tags);
                var tags = DeserializeTags(itemDto.TagsJson);
                model.Tags.Clear();
                await AddTags(model, tags);
                await transaction.CommitAsync();
            }
        }

        public async Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var item = await GetItem(itemId);
            var collectionId = (int)item.CollectionId;
            var collection = UnitOfWork.Collections.Get(itemId, v => v.User);
            await UnitOfWork.Items.Delete(itemId);
            await UnitOfWork.SaveAsync();
            return collectionId;
        }

        public async Task<LikeDTO> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(itemId, item => item.UsersLiked);
            var likeDto = new LikeDTO();
            if (item.UsersLiked.Contains(user))
            {
                item.UsersLiked.Remove(user);
                likeDto.Liked = false;
                item.Likes -= 1;
            }
            else
            {
                item.UsersLiked.Add(user);
                likeDto.Liked = true;
                item.Likes += 1;
            }
            likeDto.Count = item.Likes;
            await UnitOfWork.SaveAsync();
            return likeDto;
        }

        public async Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDTO commentDto)
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal);
            var item = await UnitOfWork.Items.Get(commentDto.ItemId);
            UnitOfWork.Comments.Add(new Comment()
            {
                ItemId = item.Id,
                Text = commentDto.Text,
                UserId = user.Id
            });
            await UnitOfWork.SaveAsync();
        }

        public IEnumerable<Item> GetLastCreatedItems()
        {
            return UnitOfWork.Context.Items.IncludeMultiple(
                item => item.UsersLiked,
                item => item.Tags,
                Item => Item.Collection)
                .AsEnumerable()
                .TakeLast(3)
                .Reverse();
        }

        public IEnumerable<TagDTO> GetTagsCloud()
        {
            var tags = UnitOfWork.Tags.Find(tag => tag.Items.Count() > 0, tag => tag.Items);
            var tagsCloud = new List<TagDTO>();
            foreach (var tag in tags)
            {
                tagsCloud.Add(new TagDTO()
                {
                    Title = tag.Title,
                    Count = UnitOfWork.Items.Find(item => item.Tags.Contains(tag)).Count()
                });
            }
            return tagsCloud;
        }

        public IEnumerable<Item> GetItemsByTag(string tag)
        {
            return UnitOfWork.Items.Find(
                item => item.Tags.Where(itemTag => itemTag.Title == tag).Count() > 0,
                includes: new Expression<Func<Item, object>>[] {
                    item => item.Collection,
                    item => item.Tags,
                    item => item.Comments,
                    item => item.UsersLiked
                });
        }

        public Task<ItemDTO> GetItem(int itemId, int page = 1, ClaimsPrincipal claimsPrincipal = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItemsFullTextSearch(string query)
        {
            throw new NotImplementedException();
        }
    }
}