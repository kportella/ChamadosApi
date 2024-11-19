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
        if (!Enum.TryParse<ETipoManutencao>(tipoManutencao, out var tipoManutencaoEnum) || Enum.IsDefined(typeof(ETipoManutencao), tipoManutencao))
            return Result.Failure<GravarChamadoCommand>("Tipo de Manutenção inválido.");

        if (!Enum.TryParse<ECriticidade>(criticidade, out var criticidadeEnum) || Enum.IsDefined(typeof(ECriticidade), criticidade))
            return Result.Failure<GravarChamadoCommand>("Criticidade inválida.");
        
        if (!Enum.TryParse<EStatus>(status, out var statusEnum) || Enum.IsDefined(typeof(EStatus), status))
            return Result.Failure<GravarChamadoCommand>("Status inválido.");

        var resultadoValidacao = Validar(titulo, descricao, tecnico, dataAbertura, dataFechamento, equipamento, 
            localizacao, modelo);
        
        if (resultadoValidacao.IsFailure)
            return Result.Failure<GravarChamadoCommand>(resultadoValidacao.Error);

        return new GravarChamadoCommand(titulo, descricao, tipoManutencaoEnum, criticidadeEnum, tecnico, dataAbertura,
            dataFechamento, statusEnum, equipamento, localizacao, modelo);
    }

    private static Result Validar(string titulo, string descricao, string tecnico, DateTime dataAbertura, 
        DateTime dataFechamento, string equipamento, string localizacao, string modelo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return Result.Failure("Titulo deve ser informado.");
        
        if (string.IsNullOrWhiteSpace(descricao))
            return Result.Failure("Descricao deve ser informado.");
        
        if (string.IsNullOrWhiteSpace(tecnico))
            return Result.Failure("Tecnico deve ser informado.");
        
        if (string.IsNullOrWhiteSpace(equipamento))
            return Result.Failure("Equipamento deve ser informado.");
        
        if (string.IsNullOrWhiteSpace(localizacao))
            return Result.Failure("Localizacao deve ser informado.");
        
        if (string.IsNullOrWhiteSpace(modelo))
            return Result.Failure("Modelo deve ser informado.");
        
        if (dataFechamento < DateTime.Now)
            return Result.Failure("Data deve ser maior que o dia de hoje.");
        
        return Result.Success();
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