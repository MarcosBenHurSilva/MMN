retorno<Int32> retAlterar;
retorno<Int32> validacoes;
//Na alteração, os campos Cod_Cliente e Tipo_Pessoa devem ser dessetados.
toRenda.CodCliente = new TORenda();

RNRenda rnRenda = Infra.InstanciarRN<RNRenda>();
Retorno<TORenda> retObter = rnRenda.Obter(toRenda);
if(!retObter.Ok)
{
    return this.Infra.RetornarFalha<Int32>(retObter.Mensagem);
}
//Verificar se o usuário alterou os campos da tela, se sim sobrescreve os campos do TOComplementado
if(!toRenda.CodCliente.FoiSetado){toRendaComplementado.CodCliente = Renda.CodCliente; }
if(!toRenda.DataInicial.FoiSetado){toRendaComplementado.DataInicial = Renda.DataInicial; }
if(!toRenda.TipoRenda.FoiSetado){toRendaComplementado.TipoRenda = Renda.TipoRenda; }
if(!toRenda.CodEmpresa.FoiSetado){toRendaComplementado.CodEmpresa = Renda.CodEmpresa; }
if(!toRenda.ValorRenda.FoiSetado){toRendaComplementado.ValorRenda = Renda.ValorRenda; }
if(!toRenda.DataTermino.FoiSetado){toRendaComplementado.DataTermino = Renda.DataTermino; }
// Joga o TOComplementado nas RNs
validacoes = regrasNegocio.RN_01(toRendaComplementado);

if(!validacoes.Ok)
{
    return validacoes;
}

using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdRenda.Alterar(toRenda);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retAlterar.Dados, new OperacaoRealizadaMensagem("Alteração"));
            

#region RN01
        /// <summary>RN01.</summary>
        /// <param name="toRenda">Transfer Object de entrada referente à tabela Renda.</param>
        /// <returns>Retorno da regra.</returns>
        public virtual Retorno<Int32> RN01(TORenda toRenda)
        {
            try
            {
                if(toRenda.TipoPessoa.LerConteudoOuPadrao() != TipoPessoa.Fisica)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.Falha_RN011.1));
                }
                if(toRenda.TipoPessoa.LerConteudoOuPadrao() == TipoPessoa.Fisica)
                {
                    ClientePxc rnClientePxc = this.Infra.InstanciarRN<ClientePxc>();
                    TOClientePxc toClientePxc = NEW TOClientePxc();
                    toClientePxc.CodCliente = toRenda.CodCliente;
                    Retorno<TOClientePxc> obtencaoClientePxc = rnClientePxc.Obter(toClientePxc);

                    if(!obtencaoClientePxc.Ok)
                    {
                        if(obtencaoClientePxc.Mensagem is RegistroInexistenteMensagem) 
                        {
                            return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.Falha_RN011.2));
                        }
                        return this.Infra.RetornarFalha<Int32>(obtencaoClientePxc.Mensagem);
                    }
                    return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem());
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
        #endregion

public void AlterarComSucesso()
{
    TORenda toRenda = new TORenda
    {
        //codRenda = ,
        codCliente = "83919961070",
        tipoCliente = TipoPessoa.Fisica,
        TipoRenda = TipoRenda.Autonomo,
        valor = 1000,
        dataInicial = "22/04/20223",
        nomeCliente = "Bruna"
        //ultAtualizacao = ;
    };

    Renda rnRenda = this.Infra.InstanciarRN<Renda>();
    Retorno<Int32> rendaIncluida = rnRenda.Incluir(toRenda);

    Retorno<TORenda> rendaRetornada = rnRenda.Obter(toRenda);

    toRenda = rendaRetornada.Dados;

    toRenda.TipoRenda = TipoRenda.Empregado;

    base.TestarAlterar(toRenda);
}
toRenda UltAtualizacao = DateTime.Parse("23/04/2023 13:02:00.123456")

RN017
if (this.LerValorCliente("data_termino") != null) 
{
    toRenda.DataTermino = Convert.toDateTime(tjis.LerValorCliente("data_termino"))
}
else 
{
    toRenda.DataTermino = new CampoOpcional<DateTime>(null);
}