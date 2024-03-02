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
        public static void UpdateFile(string filePath, List<Car> cars)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("VIN;Year;Brand;Model;Finition;DateOfPurchase;PurchasePrice;Reparations;ReparationsCost;DateOfAvailabilityToSell;SellingPrice;DateOfSale;ImageUrl;IsSold");
                foreach (var car in cars)
                {
                    string reparations = car.Reparations != null ? string.Join(",", car.Reparations) : string.Empty;
                    string formattedDateOfPurchase = Utils.DateConvertor.DateTimeToString(car.DateOfPurchase);
                    string formattedDateOfAvailabilityToSell = Utils.DateConvertor.DateTimeToString(car.DateOfAvailabilityToSell);
                    string formattedDateOfSale = Utils.DateConvertor.DateTimeToString(car.DateOfSale);
                    string isSold = car.IsSold ? "yes" : "no";
                    // Write the formatted date strings to the file
                    writer.WriteLine($"{car.VIN};{car.Year};{car.Brand};{car.Model};{car.Finition};{formattedDateOfPurchase};{car.PurchasePrice};{reparations};{car.ReparationsCost};{formattedDateOfAvailabilityToSell};{car.SellingPrice};{formattedDateOfSale};{car.ImageUrl};{isSold}");
                }
            }
        }

        public static Car? GetCarByVin(string? vin)
        {
            if (string.IsNullOrEmpty(vin))
            {
                return null;
            }
            List<Car> cars = GetAllCars();
            return cars.Find(c => c.VIN == vin);
        }

        public static List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            if (!System.IO.File.Exists(DataFilePath))
            {
                return cars;
            }

            using (var reader = new StreamReader(DataFilePath))
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }
                    var values = line?.Split(';');

                    if (values != null)
                    {
                        Car car = new Car
                        {
                            VIN = string.IsNullOrEmpty(values[0]) ? Utils.VINGenerator.GenerateVIN() : values[0],
                            Year = int.Parse(values[1]),
                            Brand = values[2],
                            Model = values[3],
                            Finition = values[4],
                            DateOfPurchase = Utils.DateConvertor.DateStringToDateTime(values[5]),
                            PurchasePrice = Utils.MoneyConvertor.EuroStringToFloat(values[6]),
                            Reparations = [.. values[7].Split(",")],
                            ReparationsCost = Utils.MoneyConvertor.EuroStringToFloat(values[8]),
                            DateOfAvailabilityToSell = Utils.DateConvertor.DateStringToDateTime(values[9]),
                            SellingPrice = Utils.MoneyConvertor.EuroStringToFloat(values[10]),
                            DateOfSale = Utils.DateConvertor.DateStringToDateTime(values[11]),
                            ImageUrl = values[12],
                            IsSold = values[13] == "yes" ? true : false
                        };
                        cars.Add(car);
                    }
                }
            }
            UpdateFile(DataFilePath, cars);
            return cars;
        }
    }
}