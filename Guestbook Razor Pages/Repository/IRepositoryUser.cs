namespace Guestbook.Repository
{
    using Guestbook.Models;
    public interface IRepositoryUser
    {
        Task<IEnumerable<User>> GetUsersToListAsync();
        User? GetUserByLogin(string login);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
