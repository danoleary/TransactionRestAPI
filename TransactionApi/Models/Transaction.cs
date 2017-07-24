using System;
using System.ComponentModel.DataAnnotations;

namespace TransactionApi.Models
{
    public class Transaction
    {
        [Key]
        public long? Id { get; set; }

        [Required]
        public DateTime? TransactionDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal? TransactionAmount {get; set; }

        [Required]
        public DateTime CreatedDate {get; set; }

        [Required]
        public DateTime ModifiedDate {get; set;}

        public string CurrencyCode {get; set;}

        public string Merchant {get; set;}
    }
}
