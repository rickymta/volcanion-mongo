using MongoDB.Driver;
using Volcanion.Sample.MongoDB.Infrastructure.Abstractions;
using Volcanion.Sample.MongoDB.Infrastructure.Implementations;
using Volcanion.Sample.MongoDB.Models.Documents;

var builder = WebApplication.CreateBuilder(args);

// Load config
var mongoSettings = builder.Configuration.GetSection("MongoDB");
var mongoClient = new MongoClient(mongoSettings["ConnectionString"]);
var database = mongoClient.GetDatabase(mongoSettings["DatabaseName"]);

// Add services to the container.
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped<IBaseRepository<ProductDocument>, ProductRepository>();

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
