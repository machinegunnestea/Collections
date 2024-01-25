using Collection.BLL.Interfaces;
using Collection.BLL.Profiles;
using Collection.BLL.Services;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Collection.BLL.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AccountProfile));
            services.AddAutoMapper(typeof(CollectionProfile));
            services.AddAutoMapper(typeof(CommentProfile));
            services.AddAutoMapper(typeof(ItemProfile));
            services.AddAutoMapper(typeof(TagProfile));
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICollectionService, CollectionService>();
            services.AddTransient<IItemService, ItemService>();
        }
    }
}