using Guestbook.Models;
using Guestbook.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace Guestbook_Razor_Pages.Pages
{
    public class SigninModel : PageModel
    {
        [BindProperty]
        public LoginModel loginModel { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromServices] IRepositoryUser repository)
        {
            if (!ModelState.IsValid) return Page();
            var user = repository.GetUserByLogin(loginModel.Login);
            string name = user?.Name!;
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Неправильний логін або пароль");
                return Page();
            }
            byte[] passwordBytes = Encoding.Unicode.GetBytes(user.Salt + loginModel.Password);
            string hash = Convert.ToHexString(SHA256.HashData(passwordBytes));
            if (hash != user.Password)
            {
                ModelState.AddModelError(String.Empty, "Неправильний логін або пароль");
                return Page();
            }
            HttpContext.Session.SetString("login", user.Login!);
            return RedirectToPage("./Index");
        }
    }
}
