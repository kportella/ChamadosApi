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
        var comando = GravarChamadoCommand.Criar(request.Titulo, request.Descricao, 
            request.TipoManutencao, request.Criticidade, request.Tecnico, request.DataAbertura, request.DataFechamento, 
            request.Status, request.Equipamento, request.Localizacao, request.Modelo);
        
        var resultHandler = await handler.Handle(comando.Value);
        
        return Ok();
    }
}