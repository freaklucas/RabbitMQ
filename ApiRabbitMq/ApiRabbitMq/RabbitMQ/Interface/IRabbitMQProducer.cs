namespace ApiRabbitMq.RabbitMQ.Interface;

public interface IRabbitMQProducer
{
    void SendProductMessage<T>(T message);
}