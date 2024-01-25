using Collection.BLL.DTO;
using Collection.DAL.Entities;
using System.Security.Claims;

namespace Collection.BLL.Interfaces
{
    public interface ICollectionService
    {
        IEnumerable<string> GetThemes();

        Task CreateCollection(ClaimsPrincipal user, CollectionDTO collectionDto, string userId = "");

        Task<CollectionDTO> GetCollection(int collectionId);

        IEnumerable<string> GetImages(int collectionId);

        Task EditCollection(ClaimsPrincipal claimsPrincipal, CollectionDTO collectionDto);

        Task DeleteCollection(ClaimsPrincipal claimsPrincipal, int collectionId);

        IEnumerable<CollectionModel> GetLagestNumberItems();

        IEnumerable<CollectionModel> GetAllCollections(int page = 1);
    }
}