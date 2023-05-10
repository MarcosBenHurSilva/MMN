public virtual Retorno Incluir(TOOperacaoCred toOperacaoCred)
{
    try
    {
        Pxcqocxn.OperacaoCred bdOperacaoCred;
        Retorno retorno;

        #region Validação de campos
        //Valida que os campos obrigatórios foram informados
        if (!toOperacaoCred.NumContrato.FoiSetado || !toOperacaoCred.ValorFinanciado.FoiSetado || !toOperacaoCred.QtdeParcelas.FoiSetado)
        {
            return this.Infra.RetornarFalha(new CampoObrigatorioMensagem("NUM_CONTRATO, VALOR_FINANCIADO e QTDE_PARCELAS"));
        }
        #endregion

        #region Validação de regras de negócio
        //Verifica outras regras de negócio, se necessário
        #endregion

        //Calcula o valor da parcela
        double taxaMensal = Math.Pow(1 + 0.011, 1.0/12) - 1;
        double valorParcela = (toOperacaoCred.ValorFinanciado.Valor * Math.Pow(1 - taxaMensal, toOperacaoCred.QtdeParcelas.Valor)) / toOperacaoCred.QtdeParcelas.Valor;
        toOperacaoCred.ValorParcela.Valor = valorParcela;

        bdOperacaoCred = this.Infra.InstanciarBD<Pxcqocxn.OperacaoCred>();

        retorno = bdOperacaoCred.Incluir(toOperacaoCred);
        if (!retorno.OK)
        {
            return retorno;
        }

        return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
    }
    catch (Exception ex)
    {
        return this.Infra.TratarExcecao(ex);
    }
}

========================================================================================================
//Obter o valor financiado e a quantidade de parcelas do objeto toOperacaoCred
decimal valorFinanciado = toOperacaoCred.ValorFinanciado;
int parcelas = toOperacaoCred.QuantidadeParcelas;

//Calcular o valor da taxa de juros mensal (1,1% am)
decimal taxaJurosMensal = 0.011m; //(0.011 = 1,1%)

//Calcular o valor da parcela com base na fórmula fornecida
decimal valorParcela = (valorFinanciado * (decimal)Math.Pow(1 - (double)taxaJurosMensal, parcelas)) / parcelas;

//Atribuir o valor da parcela ao objeto toOperacaoCred
toOperacaoCred.ValorParcela = valorParcela;
===========================================================================================================
public virtual Retorno Incluir(TOOperacaoCred toOperacaoCred)
{
    try
    {
        Pxcqocxn.OperacaoCred bdOperacaoCred;
        Retorno retornoInclusao;
        
        #region Validação de campos obrigatórios
        //Valida que os campos obrigatórios foram informados
        if (!toOperacaoCred.NumContrato.FoiSetado)
        {
            return this.Infra.RetornarFalha(new CampoObrigatorioMensagem("NUM_CONTRATO"));
        }
        //Valida que o campo "Valor Financiado" foi informado
        if (!toOperacaoCred.ValorFinanciado.FoiSetado)
        {
            return this.Infra.RetornarFalha(new CampoObrigatorioMensagem("VALOR_FINANCIADO"));
        }
        //Valida que o campo "Quantidade de Parcelas" foi informado
        if (!toOperacaoCred.QtdeParcelas.FoiSetado)
        {
            return this.Infra.RetornarFalha(new CampoObrigatorioMensagem("QTDE_PARCELAS"));
        }
        #endregion

        #region Validação de regras de negócio
        //Calcula o valor da parcela com base na quantidade de parcelas e no valor financiado
        double taxa = 0.011; //1,1% ao mês
        double valorParcela = Math.Round((toOperacaoCred.ValorFinanciado * Math.Pow(1 - taxa, toOperacaoCred.QtdeParcelas)) / toOperacaoCred.QtdeParcelas, 2);
        //Atribui o valor calculado à propriedade "Valor da Parcela" do objeto toOperacaoCred
        toOperacaoCred.ValorParcela.Valor = valorParcela;

        #endregion

        bdOperacaoCred = this.Infra.InstanciarBD<Pxcqocxn.OperacaoCred>();

        retornoInclusao = bdOperacaoCred.Incluir(toOperacaoCred);
        if (!retornoInclusao.OK)
        {
            return retornoInclusao;
        }

        return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
    }
    catch (Exception ex)
    {
        return this.Infra.TratarExcecao(ex);
    }
}
=======================================================================================================
public bool IncluirOperacaoCredito(decimal valorFinanciado, int parcelas)
{
    if (valorFinanciado < 1000)
    {
        Console.WriteLine("Valor financiado deve ser maior ou igual a R$1.000 (mil reais).");
        return false;
    }

    // Restante da lógica para incluir a operação de crédito
    // ...

    return true;
}
======================================================================================================
// Verifica se o valor financiado é maior ou igual a R$ 1000,00
if (valorFinanciado < 1000)
{
    Console.WriteLine("Valor financiado deve ser maior ou igual a R$1.000 (mil reais).");
    // Não permite o cadastramento
    return;
}
// Permite o cadastramento
// Insira o código para inclusão/alteração da operação de crédito aqui
=====================================================================================================
// Na inclusão/alteração, o valor da parcela deve ser [VALOR_FINANCIADO*(1-TAXA)^PARCELAS]/PARCELAS.
// Onde:
// P = Quantidade de Parcelas
// VF = Valor Financiado
// TAXA = 1,1% am (lembre-se de que a taxa na fórmula NÃO deve ser utilizada percentual)

function calcularValorParcela(valorFinanciado, quantidadeParcelas) {
  const taxa = 0.011; // 1,1% ao mês
  
  const valorParcela = (valorFinanciado * Math.pow(1 - taxa, quantidadeParcelas)) / quantidadeParcelas;
  
  return valorParcela;
}
===========================================================================================================
// define uma classe com DataInicio e DataFim
public class MinhaClasse {
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

// instancia um objeto da classe e atribui null aos campos
MinhaClasse objeto = new MinhaClasse();
objeto.DataInicio = null;
objeto.DataFim = null;
=============================================================================================================
public class OperacaoCredito
{
    // ... outros campos
    
    public CampoOpcional<DateTime?> DataInicio { get; } = new CampoOpcional<DateTime?>(() => DateTime.Now);
    
    // ... outros métodos
}
================================================================================================================
#region Campos Opcionais
/// <summary>Campo DATA_INICIO da tabela OPERACAO_CREDITO.</summary>
[XmlAttribute("data_inicio")]
[CampoTabela("DATA_INICIO", TipoParametro = DbType.Date, Tamanho = 4, Precisao = 4)]
public CampoOpcional<DateTime> DataInicio
{
    get
    {
        return new CampoOpcional<DateTime>(DateTime.Today);
    }
}
#endregion
=========================================================================================================================
// Verifica se a quantidade de parcelas é válida
if (toOperacaoCred.Parcelas != 1 && toOperacaoCred.Parcelas != 12 &&
    toOperacaoCred.Parcelas != 24 && toOperacaoCred.Parcelas != 48 &&
    toOperacaoCred.Parcelas != 96)
{
    Console.WriteLine("A quantidade de parcelas deve ser 1, 12, 24, 48 ou 96 apenas.");
    // Impede o cadastramento
    return;
}

// Quantidade de parcelas é válida, permite o cadastramento
// Incluir/alterar a operação de crédito
// ...
=========================================================================================================================
#region Campos Opcionais
/// <summary>Campo DATA_INICIO da tabela OPERACAO_CREDITO.</summary>
[XmlAttribute("data_inicio")]
[CampoTabela("DATA_INICIO", TipoParametro = DbType.Date, 
    Tamanho = 4, Precisao = 4)]
public CampoOpcional<DateTime> DataInicio
{
    get { return this.dataInicio; }
    set { this.dataInicio = value; }
}

/// <summary>Campo DATA_PREVISTA_FIM da tabela OPERACAO_CREDITO.</summary>
[XmlAttribute("data_prevista_fim")]
[CampoTabela("DATA_PREVISTA_FIM", TipoParametro = DbType.Date, 
    Tamanho = 4, Precisao = 4)]
public CampoOpcional<DateTime> DataPrevistaFim
{
    get
    {
        if (this.dataInicio.Valor.HasValue && this.qtdeParcelas.Valor.HasValue)
        {
            DateTime dataPrevistaFim = this.dataInicio.Valor.Value.AddMonths(this.qtdeParcelas.Valor.Value);
            return new CampoOpcional<DateTime>(dataPrevistaFim);
        }
        else
        {
            return new CampoOpcional<DateTime>();
        }
    }
}

/// <summary>Campo QTDE_PARCELAS da tabela OPERACAO_CREDITO.</summary>
[XmlAttribute("qtde_parcelas")]
[CampoTabela("QTDE_PARCELAS", TipoParametro = DbType.Int32, 
    Tamanho = 4, Precisao = 4)]
public CampoOpcional<int> QtdeParcelas
{
    get { return this.qtdeParcelas; }
    set
    {
        if (value.Valor.HasValue && 
            (value.Valor == 1 || value.Valor == 12 || value.Valor == 24 || value.Valor == 48 || value.Valor == 96))
        {
            this.qtdeParcelas = value;
        }
        else
        {
            throw new ArgumentException("A quantidade de parcelas deve ser 1, 12, 24, 48 ou 96 apenas.");
        }
    }
}
#endregion
========================================================================================================================
/// <summary>Campo DATA_PREVISTA_FIM da tabela OPERACAO_CREDITO.</summary>
[XmlAttribute("data_prevista_fim")]
[CampoTabela("DATA_PREVISTA_FIM", TipoParametro = DbType.Date, Tamanho = 4, Precisao = 4)]
public CampoOpcional<DateTime> DataPrevistaFim
{
    get
    {
        if (this.DataInicio.TemConteudo && this.QtdeParcelas.TemConteudo)
        {
            int meses = this.QtdeParcelas.LerConteudoOuPadrao();
            DateTime dataFim = this.DataInicio.LerConteudoOuPadrao().AddMonths(meses);
            return new CampoOpcional<DateTime>(dataFim);
        }
        return new CampoOpcional<DateTime>();
    }
}
===========================================================================================================================