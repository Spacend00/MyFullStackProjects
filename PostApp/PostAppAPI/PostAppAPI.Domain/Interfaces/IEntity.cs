
namespace PostAppAPI.Domain.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get;}
    }
}
