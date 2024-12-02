using Microsoft.ML;
using ExampleApi.Models; // Seus modelos de dados
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApi.Services
{
    public class MachineLearningService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly string _modelPath;

        // O MLContext é necessário para usar o ML.NET e _model é o modelo treinado
        public MachineLearningService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mlContext = new MLContext();
            _modelPath = "model.zip"; // Caminho onde o modelo treinado estará salvo

            // Se o modelo já existir, carregamos ele, caso contrário, treinamos um novo modelo
            if (System.IO.File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            }
            else
            {
                _model = TrainModel();  // Caso não exista, treinamos um novo modelo
            }
        }

        // Método para treinar um modelo simples de previsão de preços de casas
        public ITransformer TrainModel()
        {
            // Criando um conjunto de dados fictício (simulação)
            var data = new List<HouseData>
            {
                new HouseData { Size = 1500, Bedrooms = 3, Price = 400000 },
                new HouseData { Size = 1800, Bedrooms = 4, Price = 500000 },
                new HouseData { Size = 2400, Bedrooms = 3, Price = 600000 },
                new HouseData { Size = 3000, Bedrooms = 5, Price = 700000 },
                new HouseData { Size = 3500, Bedrooms = 4, Price = 750000 }
            };

            var trainData = _mlContext.Data.LoadFromEnumerable(data);

            // Definindo a pipeline de ML: features (entrada) e o alvo (preço da casa)
            var pipeline = _mlContext.Transforms.Concatenate("Features", "Size", "Bedrooms")
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", featureColumnName: "Features"));

            // Treinando o modelo
            var model = pipeline.Fit(trainData);

            // Salvando o modelo treinado
            _mlContext.Model.Save(model, trainData.Schema, _modelPath);

            return model;
        }

        // Método para fazer uma previsão com o modelo treinado
        public async Task<float> PredictPriceAsync(HouseData houseData)
        {
            // Usando PredictionEngine para fazer a previsão
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HouseData, HousePricePrediction>(_model);

            // Fazendo a previsão
            var prediction = predictionEngine.Predict(houseData);

            return prediction.Price;
        }
    }

    // Modelo de entrada para dados de casas
    public class HouseData
    {
        public float Size { get; set; }      // Tamanho da casa
        public float Bedrooms { get; set; }  // Número de quartos
        public float Price { get; set; }     // Preço da casa (usado para treino)
    }

    // Modelo de saída da previsão
    public class HousePricePrediction
    {
        public float Price { get; set; }
    }
}
