using ApiRabbitMq.Context;
using ApiRabbitMq.RabbitMQ.Impl;
using ApiRabbitMq.RabbitMQ.Interface;
using ApiRabbitMq.Services;
using ApiRabbitMq.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

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

app.Lifetime.ApplicationStopping.Register(() =>
{
    var rabbitMQProducer = app.Services.GetRequiredService<IRabbitMQProducer>() as RabbitMQProducer;
    rabbitMQProducer?.Close();
});

app.Run();
