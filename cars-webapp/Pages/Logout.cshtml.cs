using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace cars_webapp.Pages
{
    public class Logout : PageModel
    {
        private const string COOKIE_NAME = "BaseCookie";

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(COOKIE_NAME);
            return RedirectToPage("/List");
        }
    }
}