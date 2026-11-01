using Guestbook.Models;

namespace Guestbook.Repository
{
    public interface IRepositoryMessage
    {
        Task<IEnumerable<Message>> GetMessagesToListAsync();
        Task AddMessageAsync(Message message);
        Task SaveChangesAsync();
    }
}
