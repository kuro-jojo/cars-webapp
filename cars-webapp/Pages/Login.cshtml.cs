using System.Security.Claims;
using cars_webapp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages
{
    public class LoginModel : PageModel
    {
        private const string COOKIE_NAME = "BaseCookie";

        [BindProperty]
        public AdminUser? AdminUser { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // var passwordHasher = new PasswordHasher<string>();
            // Console.WriteLine(passwordHasher.HashPassword(null, "admin"));
            // Console.ReadLine();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }
            if (AdminUser != null && !string.IsNullOrEmpty(AdminUser.UserName) && string.Equals(AdminUser.UserName, "admin") && string.Equals(AdminUser.Password, "admin"))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, AdminUser.UserName)
                    };
                var claimsIdentity = new ClaimsIdentity(claims, COOKIE_NAME);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(COOKIE_NAME, principal);
                return RedirectToPage("/List");
            }
            ErrorMessage = "Invalid username or password";

            return Page();
        }
    }
}
