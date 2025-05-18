namespace TestProject1
{
    using Microsoft.ML;

    using MLOps.Training;

    public class ModelTests
    {
        [Fact]
        public void ModelLoadsAndPredicts()
        {
            var ml = new MLContext();
            var modelPath = Path.Combine(".\\..\\..\\..\\..\\MLOps.Training\\bin\\Debug\\net8.0\\model", "churn.zip");
            var model = ml.Model.Load(modelPath, out _);

            var engine = ml.Model.CreatePredictionEngine<Customer, Prediction>(model);

            var sample = new Customer { Balance = 500f, Age = 30f /*…*/ };
            var pred = engine.Predict(sample);

            // Score should be between 0 and 1
            Assert.InRange(pred.Score, -5f, 1f);
        }

        private class Prediction { public bool IsPrediction; public float Score; }
    }
}