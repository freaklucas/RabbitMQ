﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq.Expressions;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare("product", exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    try
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Mensagem do produto recebida: {message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao processar a mensagem: {ex.Message}");
    }
};

channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

Console.ReadKey();