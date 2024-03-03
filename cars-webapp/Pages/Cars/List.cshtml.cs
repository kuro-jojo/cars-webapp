using cars_webapp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cars_webapp.Pages.Cars
{

    public class ListModel : PageModel
    {
        private readonly string _dataFilePath = Data.File.DataFilePath;
        private readonly ILogger<ListModel> _logger;
        public List<Car> Cars { get; set; }

        public ListModel(ILogger<ListModel> logger)
        {
            Cars = new List<Car>();
            _logger = logger;
        }



        public void OnGet()
        {
            Cars = Data.CarRepository.GetAllCars();
        }

    }
}
