using Guestbook.Models;
using Guestbook.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace Guestbook_Razor_Pages.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegistrationModel registrationModel { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromServices] IRepositoryUser repository)
        {
            if (!ModelState.IsValid) return Page();
            byte[] saldBytes = RandomNumberGenerator.GetBytes(16);
            string salt = Convert.ToHexString(saldBytes);
            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + registrationModel.Password);
            string password = Convert.ToHexString(SHA256.HashData(passwordBytes));
            User user = new User { Login = registrationModel.Login, Name = registrationModel.Name, Surname = registrationModel.Surname, Password = password, Salt = salt, Year = registrationModel.Year };
            await repository.AddUserAsync(user);
            await repository.SaveChangesAsync();
            HttpContext.Session.SetString("login", registrationModel.Login!);
            return RedirectToPage("./Index");
        }
    }
}
