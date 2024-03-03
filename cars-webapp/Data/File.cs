using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cars_webapp.Models;

namespace cars_webapp.Data
{
    public static class File
    {
        public readonly static string DataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "cars.csv");
        public readonly static string UploadedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");

        public static void WriteIntoFile(Car car, StreamWriter writer, string lines)
        {
            if (!string.IsNullOrEmpty(lines))
            {
                writer.WriteLine(lines);
            }
            // string reparations = car.Reparations != null ? string.Join(",", car.Reparations) : string.Empty;
            string formattedDateOfPurchase = Utils.DateConvertor.DateTimeToString(car.DateOfPurchase);
            string formattedDateOfAvailabilityToSell = Utils.DateConvertor.DateTimeToString(car.DateOfAvailabilityToSell);
            string formattedDateOfSale = Utils.DateConvertor.DateTimeToString(car.DateOfSale);
            string isSold = car.IsSold ? "yes" : "no";
            string? imageUrl = Path.GetFileName(car.ImageUrl);
            // Write the formatted date strings to the file
            writer.WriteLine($"{car.VIN};{car.Year};{car.Brand};{car.Model};{car.Finition};{formattedDateOfPurchase};{car.PurchasePrice};{car.Reparations};{car.ReparationsCost};{formattedDateOfAvailabilityToSell};{car.SellingPrice};{formattedDateOfSale};{imageUrl};{isSold}");
        }

        public static string GetUploadedImage(string filename)
        {
            string[] files = Directory.GetFiles(UploadedFilePath);
            string? file = files.FirstOrDefault(f => f.Contains(filename));
            if (file != null)
            {
                return "upload/" + filename;
            }
            return string.Empty;
        }

        public static async Task CreateFile(string? imageUrl, IFormFile? Upload)
        {
            if (Upload != null && imageUrl != null)
            {
                var file = Path.Combine(UploadedFilePath, imageUrl);
                try
                {
                    using (var fileStream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}