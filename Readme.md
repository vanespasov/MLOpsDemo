# ML.NET Customer Churn Prediction Solution

This solution demonstrates a machine learning system for predicting customer churn using ML.NET, structured in a microservices architecture.

## Solution Structure

The solution consists of three main projects:

### MLOps.Training
- Contains the model training pipeline
- Handles data processing and model creation
- Outputs the trained model as `churn.zip`

### MLOps.Serving
- Web API service for model inference
- Exposes prediction endpoints
- Handles real-time customer churn predictions

### MLOps.Tests
- Contains unit tests and model validation tests

## Solution Architecture Diagram

┌─────────────────────────────┐
│       MLOps.Training        │
│                             │
│ ┌─────────────────────────┐ │
│ │     Training Pipeline   │ │          ┌─────────────────────────┐
│ │ - Data Processing       │ │          │     MLOps.Serving       │
│ │ - Model Training        │ │          │                         │
│ │ - Feature Engineering   │ │          │ ┌────────────────────┐  │
│ └─────────────────────────┘ │          │ │   Web API Service  │  │
│           │                 │          │ │  - REST Endpoints  │  │
│           │                 │          │ │  - JSON Responses  │  │
│           ▼                 │          │ └────────────────────┘  │
│    [churn.zip model]───────-┼─────────▶│          ▲              │
└─────────────────────────────┘          │          │              │
                                         │          ▼              │
                                         │ ┌────────────────────┐  │
                                         │ │  Prediction Engine │  │
                                         │ └────────────────────┘  │
                                         └─────────────────────────┘
                                                     ▲
                                                     │
                                    ┌────────────────┴──────────────┐
                                    │        MLOps.Tests            │
                                    │                               │
                                    │ ┌────────────────┐            │
                                    │ │   Unit Tests   │            │
                                    │ └────────────────┘            │
                                    │ ┌────────────────┐            │
                                    │ │  Model Tests   │            │
                                    │ └────────────────┘            │
                                    └───────────────────────────────┘

Key Features:
→ MLOps.Training: Builds and exports the ML model
→ MLOps.Serving: Hosts prediction API and model inference
→ MLOps.Tests: Ensures quality and validation
→ churn.zip: Trained model artifact

Data Flow:
1. Training pipeline processes data and creates model
2. Model is exported as churn.zip
3. Serving layer loads model for predictions
4. Tests validate both training and serving components


- ## Solution Components Diagram

+-------------------+         Trained Model         +-------------------+
|                   |  churn.zip (output file)      |                   |
|  MLOps.Training   +-----------------------------> |   MLOps.Serving   |
| (Model Training)  |                               |  (Prediction API) |
+-------------------+                               +-------------------+
                                                          ^
                                                          |
                                                          |
                                                +-------------------+
                                                |                   |
                                                |   MLOps.Tests     |
                                                | (Unit & Model     |
                                                |   Validation)     |
                                                +-------------------+



## Setup and Running

1. **Build the Solution**
   
2. **Train the Model**
   - Run the MLOps.Training project first to generate the model
   
3. **Start the Prediction Service**
   
## Using the API

The prediction service exposes a REST API endpoint for customer churn predictions.

### Make a Prediction
curl -X POST http://localhost:5000/api/predict 
-H "Content-Type: application/json" 
-d '{ "creditScore": 619, "country": "France", "gender": "Female", "age": 42, "tenure": 2, "balance": 0, "productsNumber": 1, "creditCard": true, "activeMember": true, "estimatedSalary": 101348.88 }'

### Response Format
{ "prediction": true, "score": 0.85 }

- `prediction`: Boolean indicating likelihood of churn
- `score`: Confidence score of the prediction

## Model Input Schema

The model expects the following features:
- `creditScore`: Customer's credit score
- `country`: Customer's country
- `gender`: Customer's gender
- `age`: Customer's age
- `tenure`: Length of time as a customer
- `balance`: Account balance
- `productsNumber`: Number of bank products
- `creditCard`: Has credit card (true/false)
- `activeMember`: Active member status (true/false)
- `estimatedSalary`: Estimated salary

## Notes
- Ensure all required fields are provided in the prediction request
- The API returns HTTP 400 if the customer payload is missing
- All numerical values should be within valid ranges
