using ExampleApi.Models;
using ExampleApi.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));


builder.Services.AddScoped<UserRepository>();


builder.Services.AddScoped<MachineLearningService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();


app.Run();
