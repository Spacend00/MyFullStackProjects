using PostAppAPI.Application.Interfaces;
using PostAppAPI.Infrastructure.Persistance;

namespace PostAppAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostAppAPIDbContext _context;
        public IUserRepository Users {  get; private set; }

        public IPostRepository Posts {  get; private set; }

        public UnitOfWork(PostAppAPIDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Posts = new PostRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
