namespace MLOps.Serving.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.ML;

    using MLOps.Serving.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class PredictController : Controller
    {
        private readonly PredictionEngine<Customer, ScoredCustomer> _engine;

        public PredictController(PredictionEngine<Customer, ScoredCustomer> engine)
        {
            _engine = engine;
        }

        [HttpPost]
        public ActionResult<ScoredCustomer> Post([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer payload is required.");

            var result = _engine.Predict(customer);

            // Example of logging or telemetry
            HttpContext.RequestServices
                       .GetService<ILogger<PredictController>>()
                       ?.LogInformation("Predicted Score={Score}", result.Score);

            return Ok(result);
        }
    }
}
