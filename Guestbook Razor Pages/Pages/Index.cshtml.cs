using Guestbook.Models;
using Guestbook.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Guestbook_Razor_Pages.Pages
{
    public class IndexModel(IRepositoryMessage repositoryMessage) : PageModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public async Task OnGetAsync()
        {
            Messages = await repositoryMessage.GetMessagesToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string? comment, [FromServices] IRepositoryUser repositoryUser)
        {
            if (comment == null) return Page();
            comment = comment.Trim();
            string login = HttpContext.Session.GetString("login")!;
            var user = repositoryUser.GetUserByLogin(login);
            string day = DateTime.Now.Day.ToString().Length == 1 ? $"0{DateTime.Now.Day}" : DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString().Length == 1 ? $"0{DateTime.Now.Month}" : DateTime.Now.Month.ToString();
            var message = new Message { Date = $"{day}.{month}.{DateTime.Now.Year}", Text = comment, User = user };
            await repositoryMessage.AddMessageAsync(message);
            await repositoryMessage.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
