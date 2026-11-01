using Guestbook.Models;

namespace Guestbook.Repository
{
    using Microsoft.EntityFrameworkCore;
    public class MessageRepository(GuestbookContext _db) : IRepositoryMessage
    {
        public async Task<IEnumerable<Message>> GetMessagesToListAsync()
        {
            return await _db.Messages.Include(m => m.User).AsNoTracking().ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            await _db.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
