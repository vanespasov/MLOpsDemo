namespace MLOps.Serving.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class Customer
    {
        [JsonPropertyName("balance")]
        [Range(0, double.MaxValue)]
        public float Balance { get; set; }

        [JsonPropertyName("age")]
        [Range(0, 120)]
        public float Age { get; set; }

        public float CreditScore { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public float Tenure { get; set; }
        public float ProductsNumber { get; set; }
        public bool CreditCard { get; set; }
        public bool ActiveMember { get; set; }
        public float EstimatedSalary { get; set; }
        public bool Churn { get; set; }

    }
}
