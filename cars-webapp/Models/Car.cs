using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cars_webapp.Models
{
    public class Car
    {
        public string? VIN { get; set; }
        [Required(ErrorMessage = "L'année de la voiture est requise.")]
        [Range(1990, 2024, ErrorMessage = "L'année de la voiture doit être entre 1900 et 9999.")]
        public int? Year { get; set; }
        [Required(ErrorMessage = "La marque de la voiture est requise.")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "Le modèle de la voiture est requis.")]
        public string? Model { get; set; }
        public string? Finition { get; set; }

        [Required(ErrorMessage = "La date d'achat de la voiture est requise.")]
        public DateTime? DateOfPurchase { get; set; }

        [Required(ErrorMessage = "Le prix d'achat de la voiture est requis.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix d'achat de la voiture doit être supérieur à 0.")]
        public float PurchasePrice { get; set; }

        public string? Reparations { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix de réparation de la voiture doit être supérieur à 0.")]
        public float ReparationsCost { get; set; }

        [Required(ErrorMessage = "La date de disponibilité à la vente de la voiture est requise.")]
        public DateTime? DateOfAvailabilityToSell { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix de vente de la voiture doit être supérieur à 0.")]
        public float SellingPrice { get; set; }

        public DateTime? DateOfSale { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsSold { get; set; }

    }
}