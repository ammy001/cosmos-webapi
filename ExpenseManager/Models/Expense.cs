using System;
using System.Text.Json.Serialization;

namespace ExpenseManager.Models
{
    public class Expense
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("paymentType")]
        public string PaymentType { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}
