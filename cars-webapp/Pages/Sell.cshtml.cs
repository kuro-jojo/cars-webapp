using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages
{
    public class Sell : PageModel
    {
        private readonly ILogger<Sell> _logger;

        public Sell(ILogger<Sell> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(string? vin)
        {
            // Get the car by VIN
            Car? car = Data.CarRepository.GetCarByVin(vin);
            if (car != null)
            {
                // Set the car as sold
                car.IsSold = true;
                // Set the date of sale
                car.DateOfSale = DateTime.Now;
                // Update the car in the file
                Data.CarRepository.Update(car, null);
            }
            return RedirectToPage("/List");
        }
    }
}