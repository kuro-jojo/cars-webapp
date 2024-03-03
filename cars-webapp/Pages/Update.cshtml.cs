using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using cars_webapp.Data;

namespace cars_webapp.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ILogger<UpdateModel> _logger;
        [BindProperty]
        public string? VIN { get; set; }

        [BindProperty]
        public Car? Car { get; set; }

        [BindProperty]
        // [Required(ErrorMessage = "L'image de la voiture est requise.")]
        public IFormFile? UploadedFile { get; set; }
        public UpdateModel(ILogger<UpdateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string vin)
        {
            VIN = vin;
            Car = CarRepository.GetCarByVin(VIN);

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CarRepository.Update(Car, UploadedFile);
            // Data.File.UpdateModel(car);
            return RedirectToPage("/List");
        }

    }
}