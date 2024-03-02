using System;
using System.Collections;
using System.Collections.Generic;

namespace cars_webapp.Models
{
    public class Car
    {
        public string? VIN { get; set; }
        public int Year { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string Finition { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public float PurchasePrice { get; set; }
        public List<string>? Reparations { get; set; }
        
        public float ReparationsCost { get; set; }
        public DateTime? DateOfAvailabilityToSell { get; set; }
        public float SellingPrice { get; set; }
        public DateTime? DateOfSale { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsSold { get; set; }


        public static string ListToString(List<string>? list)
        {
            if (list == null)
            {
                return string.Empty;
            }
            return string.Join(", ", list);
        }

    }
}