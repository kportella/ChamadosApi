using System.Text;
using System.Text.Json;
using ChamadosApi.Dominio;
using CSharpFunctionalExtensions;
using RabbitMQ.Client;

namespace ChamadosApi.Infraestrutura;

public class RabbitMQProducer
{

    public async Task<Result> ProduceMessage(Chamado chamado)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: "fila_chamados",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(chamado);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty,
            routingKey: "fila_chamados",
            body: body);
            
        return Result.Success();
    }
}