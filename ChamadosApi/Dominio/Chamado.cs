using ChamadosApi.Dominio.Aplicacao;

namespace ChamadosApi.Dominio;

public record Chamado(string Titulo, string Descricao, ETipoManutencao TipoManutencao, ECriticidade Criticidade, 
    string Tecnico, DateTime DataAbertura, DateTime DataFechamento, EStatus Status, string Equipamento, 
    string Localizacao, string Modelo);