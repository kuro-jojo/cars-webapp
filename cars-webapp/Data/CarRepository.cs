using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using cars_webapp.Models;
using Microsoft.Extensions.Hosting.Internal;

namespace cars_webapp.Data
{
    public class CarRepository
    {
        private static string DataFilePath = File.DataFilePath;

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
                            Reparations = values[7],
                            ReparationsCost = Utils.MoneyConvertor.EuroStringToFloat(values[8]),
                            DateOfAvailabilityToSell = Utils.DateConvertor.DateStringToDateTime(values[9]),
                            SellingPrice = Utils.MoneyConvertor.EuroStringToFloat(values[10]),
                            DateOfSale = Utils.DateConvertor.DateStringToDateTime(values[11]),
                            ImageUrl = values[12],
                            IsSold = values[13] == "yes" ? true : false
                        };
                        car.ImageUrl = File.GetUploadedImage(car.ImageUrl);
                        cars.Add(car);
                    }
                }
            }
            // UpdateAll(DataFilePath, cars);
            return cars;
        }
        // public static async void SaveImage(Car car)
        // {
        //     string fileName = $"{car.VIN}_{car.Brand}_{car.Model}.png";
        //     string filePath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/upload", fileName);
        //     if (!System.IO.File.Exists(filePath))
        //     {
        //         Console.WriteLine("Saving image " + filePath);
        //         using (HttpClient client = new HttpClient())
        //         {
        //             client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537");
        //             HttpResponseMessage response = await client.GetAsync(car.ImageUrl);
        //             Console.WriteLine("Response " + response.StatusCode);
        //             response.EnsureSuccessStatusCode();
        //             byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
        //             await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
        //             Console.WriteLine("Save into " + filePath);
        //         }
        //     }
        // }

        public static async void Add(Car? car, IFormFile? uploadedFile)
        {
            if (car != null)
            {
                if (!System.IO.File.Exists(DataFilePath))
                {
                    // create the file
                    using (var writer = new StreamWriter(DataFilePath))
                    {
                        writer.WriteLine("VIN;Year;Brand;Model;Finition;DateOfPurchase;PurchasePrice;Reparations;ReparationsCost;DateOfAvailabilityToSell;SellingPrice;DateOfSale;ImageUrl;IsSold");
                    }
                }

                using (var reader = new StreamReader(DataFilePath))
                {
                    var lines = reader.ReadToEnd();
                    using (var writer = new StreamWriter(DataFilePath))
                    {
                        car.VIN = Utils.VINGenerator.GenerateVIN();
                        car.ImageUrl = $"{car.VIN}_{car.Brand}_{car.Model}.png".Replace(" ", "");
                        Console.WriteLine("Image : " + car.ImageUrl);
                        await File.CreateFile(car.ImageUrl, uploadedFile);
                        File.WriteIntoFile(car, writer, lines);
                    }
                }
            }
        }
        public static async void Update(Car? car, IFormFile? uploadedFile)
        {
            if (car != null)
            {
                if (!System.IO.File.Exists(DataFilePath))
                {
                    // create the file
                    using (var writer = new StreamWriter(DataFilePath))
                    {
                        writer.WriteLine("VIN;Year;Brand;Model;Finition;DateOfPurchase;PurchasePrice;Reparations;ReparationsCost;DateOfAvailabilityToSell;SellingPrice;DateOfSale;ImageUrl;IsSold");
                    }
                }

                var tempFile = Path.GetTempFileName();
                string? previousFile;
                using (var reader = new StreamReader(DataFilePath))
                using (var writer = new StreamWriter(tempFile))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        if (fields[0] == car.VIN)
                        {
                            previousFile = fields[12];
                            car.ImageUrl = $"{car.VIN}_{car.Brand}_{car.Model}.png".Replace(" ", "");
                            if (uploadedFile != null)
                            {
                                System.IO.File.Delete(Path.Combine(File.UploadedFilePath, previousFile));
                                await File.CreateFile(car.ImageUrl, uploadedFile);
                            }
                            else if (System.IO.File.Exists(Path.Combine(File.UploadedFilePath, previousFile)))
                            {
                                System.IO.File.Move(Path.Combine(File.UploadedFilePath, previousFile), Path.Combine(File.UploadedFilePath, car.ImageUrl));
                            }
                            File.WriteIntoFile(car, writer, "");
                        }
                        else
                        {
                            line = string.Join(";", fields);
                            writer.WriteLine(line);
                        }
                    }
                    System.IO.File.Delete(DataFilePath);
                    System.IO.File.Move(tempFile, DataFilePath);
                }
            }
        }
        public static void UpdateAll(string filePath, List<Car> cars)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("VIN;Year;Brand;Model;Finition;DateOfPurchase;PurchasePrice;Reparations;ReparationsCost;DateOfAvailabilityToSell;SellingPrice;DateOfSale;ImageUrl;IsSold");
                foreach (var car in cars)
                {
                    File.WriteIntoFile(car, writer, "");
                }
            }
        }
    }
}