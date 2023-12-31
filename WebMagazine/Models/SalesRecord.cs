﻿using System.ComponentModel.DataAnnotations;

namespace WebMagazine.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public SaleStatus Status { get; set; }
        /* Define relação com vendedor */
        [Display(Name = "Seller")]
        public int SellerId { get; set; }
        public Seller Seller { get; set; }

    }

    public enum SaleStatus : int
    {
        PENDING = 0,
        BILLED = 1,
        CANCELED = 2
    }

}
