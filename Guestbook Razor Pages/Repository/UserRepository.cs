namespace Guestbook.Repository
{
    using Guestbook.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository(GuestbookContext _db) : IRepositoryUser
    {
        public async Task<IEnumerable<User>> GetUsersToListAsync()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public User? GetUserByLogin(string login)
        {
            return _db.Users.FirstOrDefault(u => u.Login == login);
        }

        public async Task AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
