using ChamadosApi.Infraestrutura;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ChamadosApi.Dominio.Aplicacao;

public class GravarChamadoHandler(RabbitMQProducer producer)
{
    public async Task<Result> Handle(GravarChamadoCommand command)
    {
        var chamado = new Chamado(command.Titulo, command.Descricao, command.TipoManutencao, command.Criticidade, 
            command.Tecnico, command.DataAbertura, command.DataFechamento, command.Status, command.Equipamento, 
            command.Localizacao, command.Modelo);
        
        // Enviar para a fila do rabbitMQ
        await producer.ProduceMessage(chamado);
        
        return Result.Success();
    }
}