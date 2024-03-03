using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using cars_webapp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [BindProperty]
        public AdminUser AdminUser { get; set; }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // var passwordHasher = new PasswordHasher<string>();
            // Console.WriteLine(passwordHasher.HashPassword(null, "admin"));
            // Console.ReadLine();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = configuration.GetSection("AdminUser").Get<AdminUser>();

            if (user != null && UserName == user.UserName)
            {
                var passwordHasher = new PasswordHasher<string>();
                if (!string.IsNullOrEmpty(user.Password) &&
                     passwordHasher.VerifyHashedPassword("", user.Password, Password) == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToPage("/List");
                }
            }
            ErrorMessage = "Invalid username or password";
            return Page();
        }
    }
}
