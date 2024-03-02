using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages.Cars
{

    public class ListCarsModel : PageModel
    {
        private readonly string _dataFilePath = Data.File.DataFilePath;
        private readonly ILogger<ListCarsModel> _logger;
        public List<Car> Cars { get; set; }

        public ListCarsModel(ILogger<ListCarsModel> logger)
        {
            Cars = new List<Car>();
            _logger = logger;
        }

        

        public void OnGet()
        {
            Cars = Data.File.GetAllCars();
        }

    }
}
