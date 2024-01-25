using Collection.BLL.DTO;
using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using System.Security.Claims;

namespace Collection.BLL.Interfaces
{
    public interface IItemService
    {
        IUnitOfWork UnitOfWork { get; }

        Task CreateItem(ClaimsPrincipal userPrincipal, ItemDTO itemDto, string userId = "");

        Task<ItemDTO> GetItem(int itemId, int page = 1, ClaimsPrincipal claimsPrincipal = null);

        Task EditItem(ClaimsPrincipal claimsPrincipal, ItemDTO itemDto);

        Task<int> DeleteItem(ClaimsPrincipal claimsPrincipal, int itemId);

        Task<LikeDTO> LikeItem(ClaimsPrincipal claimsPrincipal, int itemId);

        Task AddComment(ClaimsPrincipal claimsPrincipal, CommentDTO commentDto);

        IEnumerable<Item> GetLastCreatedItems();

        IEnumerable<TagDTO> GetTagsCloud();

        IEnumerable<Item> GetItemsByTag(string tag);

        IEnumerable<Item> GetItemsFullTextSearch(string query);
    }
}