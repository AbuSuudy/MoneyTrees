using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTrees.Models
{
    [Table("Transactions")]
    public class TransactionData
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public Decimal? Longitude { get; set; }

        public Decimal? Latitude { get; set; }

        public long Amount { get; set; }

        public string Currency { get; set; }

        public string Category { get; set; }

        public string Logo { get; set; }

        public string Emoji { get; set; }

 

        

    }
}