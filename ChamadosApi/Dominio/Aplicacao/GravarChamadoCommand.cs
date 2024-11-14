using CSharpFunctionalExtensions;

namespace ChamadosApi.Dominio.Aplicacao;

public class GravarChamadoCommand
{
    private GravarChamadoCommand(string titulo, string descricao, ETipoManutencao tipoManutencao, ECriticidade criticidade, string tecnico, DateTime dataAbertura, DateTime dataFechamento, EStatus status, string equipamento, string localizacao, string modelo)
    {
        Titulo = titulo;
        Descricao = descricao;
        TipoManutencao = tipoManutencao;
        Criticidade = criticidade;
        Tecnico = tecnico;
        DataAbertura = dataAbertura;
        DataFechamento = dataFechamento;
        Status = status;
        Equipamento = equipamento;
        Localizacao = localizacao;
        Modelo = modelo;
    }

    public string Titulo { get;}
    public string Descricao { get;}
    public ETipoManutencao TipoManutencao { get;}
    public ECriticidade Criticidade { get;}
    public string Tecnico {get;}
    public DateTime DataAbertura { get;}
    public DateTime DataFechamento { get;}
    public EStatus Status { get;}
    public string Equipamento { get;}
    public string Localizacao { get;}
    public string Modelo { get;}

    public static Result<GravarChamadoCommand> Criar(string titulo, string descricao, string tipoManutencao, string criticidade,
        string tecnico, DateTime dataAbertura, DateTime dataFechamento, string status, string equipamento,
        string localizacao, string modelo)
    {
        var tipoManutencaoEnum = Enum.Parse<ETipoManutencao>(tipoManutencao);
        var criticidadeEnum = Enum.Parse<ECriticidade>(criticidade);
        var statusEnum = Enum.Parse<EStatus>(status);

        return new GravarChamadoCommand(titulo, descricao, tipoManutencaoEnum, criticidadeEnum, tecnico, dataAbertura,
            dataFechamento, statusEnum, equipamento,
            localizacao, modelo);
    }
}

public enum ETipoManutencao
{
    Preventiva = 1,
    Corretiva = 2,
    Preditiva = 3
}

public enum ECriticidade
{
    Baixa = 1,
    Media = 2,
    Alta = 3
}

public enum EStatus
{
    Aberto = 1,
    Fechado = 0
}