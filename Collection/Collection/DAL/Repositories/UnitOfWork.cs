using Collection.DAL.Entities;
using Collection.DAL.Interfaces;
using Collection.Data;
using Microsoft.AspNetCore.Identity;

namespace Collection.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IBaseInterface<CollectionModel> _collectionRepository;
        private IBaseInterface<Comment> _commentRepository;
        private IBaseInterface<Image> _imageRepository;
        private IBaseInterface<Item> _itemRepository;
        private IBaseInterface<Tag> _tagRepository;
        private IUserInterface<ApplicationUser> _userRepository;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<ApplicationUser> _signInManager;
        private bool disposed = false;
        private ApplicationUser _currentUser;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }

        public IBaseInterface<CollectionModel> Collections
        {
            get
            {
                _collectionRepository = _collectionRepository ?? new CollectionRepository(_context);
                return _collectionRepository;
            }
        }

        public IBaseInterface<Comment> Comments
        {
            get
            {
                _commentRepository = _commentRepository ?? new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IBaseInterface<Image> Images
        {
            get
            {
                _imageRepository = _imageRepository ?? new ImageRepository(_context);
                return _imageRepository;
            }
        }

        public IBaseInterface<Item> Items
        {
            get
            {
                _itemRepository = _itemRepository ?? new ItemRepository(_context);
                return _itemRepository;
            }
        }

        public IBaseInterface<Tag> Tags
        {
            get
            {
                _tagRepository = _tagRepository ?? new TagRepository(_context);
                return _tagRepository;
            }
        }

        public IUserInterface<ApplicationUser> Users
        {
            get
            {
                _userRepository = _userRepository ?? new UserRepository(_context);
                return _userRepository;
            }
        }

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager;
            }
        }

        public SignInManager<ApplicationUser> SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}