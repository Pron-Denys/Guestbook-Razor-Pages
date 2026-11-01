namespace Guestbook.Repository
{
    public static class MessageRepositoryExtensions
    {
        public static void AddMessageRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryMessage, MessageRepository>();
        }
    }
}
