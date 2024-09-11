using ApiRabbitMq.RabbitMQ.Interface;
using Azure.Core.Serialization;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ApiRabbitMq.RabbitMQ.Impl;

public class RabbitMQProducer : IRabbitMQProducer
{
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQProducer()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("product", exclusive: false);
    }
    
    public void SendProductMessage<T>(T message)
    {
        try
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
        }
    }


    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}
