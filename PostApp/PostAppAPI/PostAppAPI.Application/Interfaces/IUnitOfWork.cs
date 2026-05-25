
namespace PostAppAPI.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }

        Task<int> SaveChangesAsync();
    }
}
