using ChamadosApi.Dominio.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ChamadosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GravarChamadoController : ControllerBase
{
    public record struct GravarChamadoRequest(string Titulo, string Descricao, string TipoManutencao, string Criticidade, 
        string Tecnico, DateTime DataAbertura, DateTime DataFechamento, string Status, string Equipamento, 
        string Localizacao, string Modelo);
    
    [HttpPost("GravarChamado")]
    public async Task<IActionResult> GravarChamado([FromBody] GravarChamadoRequest request, [FromServices] GravarChamadoHandler handler)
    {
        // Chama classe de comando que valida as informações de entrada
        var comandoResult = GravarChamadoCommand.Criar(request.Titulo, request.Descricao, 
            request.TipoManutencao, request.Criticidade, request.Tecnico, request.DataAbertura, request.DataFechamento, 
            request.Status, request.Equipamento, request.Localizacao, request.Modelo);
        
        // Verifica se o comando está valido
        if (comandoResult.IsFailure)
            return BadRequest(comandoResult.Error);
        
        // Chama handler para enviar para fila.
        var resultHandler = await handler.Handle(comandoResult.Value);
        
        // Verifica se foi enviado corretamente
        if (resultHandler.IsFailure)
            return BadRequest(resultHandler.Error);
        
        return Ok("Chamado enviado a fila com sucesso!");
    }
}