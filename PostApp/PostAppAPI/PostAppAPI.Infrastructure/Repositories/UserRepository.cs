
using PostAppAPI.Application.Interfaces;
using PostAppAPI.Domain.Entities;
using PostAppAPI.Infrastructure.Persistance;

namespace PostAppAPI.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PostAppAPIDbContext context) : base(context) { }
    }
}
