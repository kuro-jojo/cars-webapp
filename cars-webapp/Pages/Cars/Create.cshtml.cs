using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        [BindProperty]
        public Car? Car { get; set; }
        [BindProperty]
        public IFormFile? UploadedFile { get; set; }
        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Car = new Car();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Data.CarRepository.Add(Car, UploadedFile);
            return RedirectToPage("/List");
        }

    }
}