namespace MLOps.Training
{
    using System.Text.Json;

    using Microsoft.ML;
    using Microsoft.ML.AutoML;
    internal class Program
    {
        static void Main(string[] args)
        {
            var ml = new MLContext(seed: 42);
            var data = ml.Data.LoadFromTextFile<Customer>("data/bank_churn.csv", hasHeader: true, separatorChar: ',');
            var split = ml.Data.TrainTestSplit(data, testFraction: 0.2);

            // 1) AutoML search
            var experiment = ml.Auto().CreateBinaryClassificationExperiment(120);
            var result = experiment.Execute(split.TrainSet, labelColumnName: nameof(Customer.Churn));
            var model = result.BestRun.Model;

            // 2) Evaluate
            var preds = model.Transform(split.TestSet);
            var metrics = ml.BinaryClassification.Evaluate(preds, labelColumnName: nameof(Customer.Churn));
            Console.WriteLine($"AUC={metrics.AreaUnderRocCurve:F3}  F1={metrics.F1Score:F3}");

            // 3) Gate on quality
            if (metrics.AreaUnderRocCurve < 0.75)
                throw new Exception("Model quality too low – fail the build!");

            // 4) Export model + metrics
            Directory.CreateDirectory("model");
            ml.Model.Save(model, split.TrainSet.Schema, "model/churn.zip");
            File.WriteAllText("model/metrics.json", JsonSerializer.Serialize(metrics));
        }
    }
}
