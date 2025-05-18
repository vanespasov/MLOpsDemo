namespace MLOps.Serving.Models
{
    public record ScoredCustomer
    {
        public bool Prediction { get; init; }
        public float Score { get; init; }
    }
}
