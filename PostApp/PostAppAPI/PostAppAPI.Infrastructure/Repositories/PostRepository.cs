
using PostAppAPI.Application.Interfaces;
using PostAppAPI.Domain.Entities;
using PostAppAPI.Infrastructure.Persistance;

namespace PostAppAPI.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post> , IPostRepository
    {
        public PostRepository(PostAppAPIDbContext context) : base(context) { }
    }
}
