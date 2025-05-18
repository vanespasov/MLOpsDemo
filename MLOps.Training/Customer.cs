namespace MLOps.Training
{
    using Microsoft.ML.Data;

    public class Customer
    {
        [LoadColumn(1)] public float CreditScore { get; set; }
        [LoadColumn(2)] public string Country { get; set; }
        [LoadColumn(3)] public string Gender { get; set; }
        [LoadColumn(4)] public float Age { get; set; }
        [LoadColumn(5)] public float Tenure { get; set; }
        [LoadColumn(6)] public float Balance { get; set; }
        [LoadColumn(7)] public float ProductsNumber { get; set; }
        [LoadColumn(8)] public bool CreditCard { get; set; }
        [LoadColumn(9)] public bool ActiveMember { get; set; }
        [LoadColumn(10)] public float EstimatedSalary { get; set; }
        [LoadColumn(11)] public bool Churn { get; set; }
    }
}
