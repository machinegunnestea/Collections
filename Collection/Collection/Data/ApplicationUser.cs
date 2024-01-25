using Collection.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Collection.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<CollectionModel> CollectionModels { get; set; } = new List<CollectionModel>();
        public ICollection<Item> LikedItems { get; set; } = new List<Item>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}