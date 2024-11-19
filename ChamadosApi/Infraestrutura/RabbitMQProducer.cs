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
        
        // Cria uma fábrica de conexão para conectar ao rabbit
        var factory = new ConnectionFactory { HostName = "localhost" };
        
        // Cria a conexão e o canal de forma assincrona
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        var args = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "dlx_chamados" }
        };
        
        // Declara a fila
        // Durable: Mensagem fica no disco, mesmo quando o rabbit é reinicializado
        // Exclusive: Define que a fila não é exclusiva a essa conexão
        // AutoDelete: Fila não vai ser delatada automaticante quando não tiver mais consumidores
        // Arguments: Define os valores do dead letter exchange
        await channel.QueueDeclareAsync(
            queue: "fila_chamados",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: args);

        // Objeto é serializado e transformado em um array de bytes
        // Marshaling
        var message = JsonSerializer.Serialize(chamado);
        var body = Encoding.UTF8.GetBytes(message);

        // Publica a mensagem
        // Exchance: Vazio significa que a mensagem vai ser enviada diretamente para a fila do routingKey
        // RoutingKey: Nome da fila onde vai ser enviada a mensagem
        await channel.BasicPublishAsync(exchange: string.Empty,
            routingKey: "fila_chamados",
            body: body);
            
        return Result.Success();
    }
}