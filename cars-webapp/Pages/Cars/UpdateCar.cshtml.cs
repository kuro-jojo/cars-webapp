using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages.Cars
{
    public class UpdateCar : PageModel
    {
        private readonly ILogger<UpdateCar> _logger;
        private string? Vin;

        [BindProperty]
        public Car? Car { get; set; }

        public UpdateCar(ILogger<UpdateCar> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Request.Query.TryGetValue("vin", out var vin);
            Vin = vin;
            Car = Data.File.GetCarByVin(Vin);

        }

        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Data.File.UpdateCar(car);
            return RedirectToPage("/Cars/CarList");
        }

    }
}