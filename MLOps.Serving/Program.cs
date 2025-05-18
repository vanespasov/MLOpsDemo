
namespace MLOps.Serving
{
    using Microsoft.ML;

    using MLOps.Serving.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton(sp =>
            {
                var ml = new MLContext();
                var modelPath = Path.Combine(".\\..\\MLOps.Training\\bin\\Debug\\net8.0\\model", "churn.zip");
                var model = ml.Model.Load(modelPath, out _);
                return ml.Model.CreatePredictionEngine<Customer, ScoredCustomer>(model);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
