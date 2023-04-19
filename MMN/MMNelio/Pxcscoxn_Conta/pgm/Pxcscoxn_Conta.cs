using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqcoxn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>
    /// Classe de acesso a tabela CONTA
    /// </summary>
    public class RNConta : AplicacaoRegraNegocio
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toConta">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOConta>> Listar(TOConta toConta)
        {
            try
            {
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<List<TOConta>> retListar = bdConta.Listar(toConta);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOConta>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOConta>>(retListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOConta>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toConta">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        /// 
        public Retorno<Int32> Incluir(TOConta toConta)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toConta.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toConta.Saldo.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("SALDO"));
                }
                if (!toConta.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //regras de negócio

                //RN1
                if (toConta.CodAgencia.LerConteudoOuPadrao() <= 0) //true
                {
                    return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.AgenciaInvalida));
                }
                //RN2
                if (toConta.CodConta.LerConteudoOuPadrao() <= 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.ContaInvalida));
                }
                //RN3
                if (toConta.TipoPessoa.LerConteudoOuPadrao() == "F")//PF
                {
                    if (toConta.CodEspecie.LerConteudoOuPadrao() != 35)
                    {
                        return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.EspecieInvalida));
                    }
                }
                else//PJ
                {
                    if (toConta.CodEspecie.LerConteudoOuPadrao() != 6)
                    {
                        return this.Infra.RetornarFalha<Int32>(new MensagemConta(TipoFalha.EspecieInvalida));
                    }
                }
                //RN4
                if (toConta.Limite.FoiSetado)
                {
                    //tem limite
                    if (toConta.Limite.TemConteudo && 
                            toConta.Limite.LerConteudoOuPadrao() <= 0)
                    {
                        return this.Infra.RetornarFalha<Int32>(
                            new MensagemConta(TipoFalha.LimiteInvalido));
                    }
                }
                //RN5 - saldo inicial zero
                //se o saldo for diferente de zero
                // ou o saldo for nulo ou o saldo não tiver sido informado
                //->então, dar mensagem de erro
                if(/*!toConta.Saldo.TemConteudo ||*/
                    toConta.Saldo.LerConteudoOuPadrao() != 0)
                {
                    //dar a mensagem
                    return this.Infra.RetornarFalha<Int32>(
                            new MensagemConta(TipoFalha.SaldoZero));
                }

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdConta.Incluir(toConta);
                    if (!retIncluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retIncluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retIncluir.Dados, new OperacaoRealizadaMensagem("Inclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //regras de negócio
                //RN4
                if (toConta.Limite.FoiSetado)
                {
                    //tem limite
                    if (toConta.Limite.TemConteudo &&
                            toConta.Limite.LerConteudoOuPadrao() <= 0)
                    {
                        return this.Infra.RetornarFalha<Int32>(
                            new MensagemConta(TipoFalha.LimiteInvalido));
                    }
                }

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toConta);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retAlterar.Dados, new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toConta">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdConta.Excluir(toConta);
                    if (!retExcluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retExcluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retExcluir.Dados, new OperacaoRealizadaMensagem("Exclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Efetua o saque na conta especificada 
        /// </summary>
        /// <param name="toConta">TOConta para efetuar o saque</param>
        /// <returns></returns>
        public Retorno<TOConta> Sacar(TOConta toConta)
        {
            #region Validação de campos obrigatórios
            if (!toConta.CodAgencia.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
            }
            if (!toConta.CodEspecie.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
            }
            if (!toConta.CodConta.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
            }
            if (!toConta.ValorTransacao.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("Valor da transação"));
            }
            #endregion

            //codAgencia
            //codEspecie
            //codConta
            //valorTransacao            

            //RN6
            if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
            {
                return this.Infra.RetornarFalha<TOConta>(new MensagemConta(TipoFalha.ValorTransacao));
            }

            //RN7
            //Recuperar o saldo da conta.
            TOConta toContaFiltro = new TOConta();
            toContaFiltro.CodAgencia = toConta.CodAgencia;
            toContaFiltro.CodConta = toConta.CodConta;
            toContaFiltro.CodEspecie = toConta.CodEspecie;

            Retorno<List<TOConta>> retornoConsulta = this.Listar(toContaFiltro);

            if (!retornoConsulta.Ok)
            {
                return this.Infra.RetornarFalha<TOConta>(retornoConsulta.Mensagem);
            }

            if (retornoConsulta.Dados.Count == 0)
            {
                return this.Infra.RetornarFalha<TOConta>(new MensagemConta(TipoFalha.ContaInexistente));
            }
            TOConta toContaListar = retornoConsulta.Dados[0];

            Double saldoconsiderado = toContaListar.Saldo.LerConteudoOuPadrao()
                                    + toContaListar.Limite.LerConteudoOuPadrao();

            //saldo insuficiente
            if (saldoconsiderado - 
                toConta.ValorTransacao.LerConteudoOuPadrao() < 0)
            {
                return this.Infra.RetornarFalha<TOConta>(
                    new MensagemConta(TipoFalha.SaldoInsuficiente,
                    saldoconsiderado.ToString("N"))
                    );
            }

            //Saldo suficiente ?
            Double saldoAnterior = toContaListar.Saldo;
            toContaFiltro.Saldo = (saldoAnterior - toConta.ValorTransacao.LerConteudoOuPadrao());
            BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
            Retorno<Int32> retornoAlteracao;

            using (EscopoTransacional escopo = 
                        this.Infra.CriarEscopoTransacional())
            {
                retornoAlteracao = bdConta.Alterar(toContaFiltro);
                if (!retornoAlteracao.Ok)
                {
                    return this.Infra.RetornarFalha<TOConta>(retornoAlteracao.Mensagem);
                }
                escopo.EfetivarTransacao();
            }
            return this.Infra.RetornarSucesso<TOConta>(toContaFiltro, new OperacaoRealizadaMensagem());
        }

        /// <summary>
        /// Efetua o depósito na conta especificada 
        /// </summary>
        /// <param name="toConta">TOConta para efetuar o depósito</param>
        /// <returns></returns>
        public Retorno<TOConta> Depositar(TOConta toConta)
        {
            #region Validação de campos obrigatórios
            if (!toConta.CodAgencia.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
            }
            if (!toConta.CodEspecie.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
            }
            if (!toConta.CodConta.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
            }
            if (!toConta.ValorTransacao.TemConteudo)
            {
                return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("Valor da transação"));
            }
            #endregion

            //codAgencia
            //codEspecie
            //codConta
            //valorTransacao            

            //RN6
            if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
            {
                return this.Infra.RetornarFalha<TOConta>(
                    new MensagemConta(TipoFalha.ValorTransacao));
            }

            //RN7
            //Recuperar o saldo da conta.
            TOConta toContaFiltro = new TOConta();
            toContaFiltro.CodAgencia = toConta.CodAgencia;
            toContaFiltro.CodConta = toConta.CodConta;
            toContaFiltro.CodEspecie = toConta.CodEspecie;

            Retorno<List<TOConta>> retornoConsulta = this.Listar(toContaFiltro);
            
            if (!retornoConsulta.Ok)
            {
                return this.Infra.RetornarFalha<TOConta>(
                    retornoConsulta.Mensagem);
            }

            if (retornoConsulta.Dados.Count == 0)
            {
                return this.Infra.RetornarFalha<TOConta>(
                    new MensagemConta(TipoFalha.ContaInexistente));
            }
            toContaFiltro.Saldo =
                retornoConsulta.Dados[0].Saldo.LerConteudoOuPadrao() + 
                toConta.ValorTransacao.LerConteudoOuPadrao();

            BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
            //variavel declarada.
            Retorno<Int32> retornoAlteracao;

            //sintaxe using
            //using ()
            //{

            //}

            using (EscopoTransacional escopo =
                        this.Infra.CriarEscopoTransacional())
            {
                retornoAlteracao = bdConta.Alterar(toContaFiltro);
                if (!retornoAlteracao.Ok)
                {
                    return this.Infra.RetornarFalha<TOConta>(retornoAlteracao.Mensagem);                    
                }
                escopo.EfetivarTransacao();
            }

            return this.Infra.RetornarSucesso<TOConta>(toContaFiltro, new OperacaoRealizadaMensagem());
        }

        #endregion
    }
}
