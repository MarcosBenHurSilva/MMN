using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqcrxn;

namespace Bergs.Pxc.Pxcscrxn
{
    /// <summary>
    /// Classe de acesso a tabela CREDITO
    /// </summary>
    public class RNCredito : AplicacaoRegraNegocio
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCredito">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOCredito>> Listar(TOCredito toCredito)
        {
            try
            {
                //TODO: regras de negócio
                BDCredito bdCredito = this.Infra.InstanciarBD<BDCredito>();
                Retorno<List<TOCredito>> retListar = bdCredito.Listar(toCredito);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCredito>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOCredito>>(retListar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCredito>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toCredito">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOCredito toCredito)
        {
            try
            {
                #region Validação de campos obrigatórios
                if (!toCredito.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toCredito.DataCredito.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("DATA_CREDITO"));
                }
                if (!toCredito.TempoFinanc.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TEMPO_FINANC"));
                }
                if (!toCredito.TipoImovel.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_IMOVEL"));
                }
                if (!toCredito.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCredito.ValorFinanc.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_FINANC"));
                }
                if (!toCredito.CodCredito.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CREDITO"));
                }
                #endregion
                //TODO: regras de negócio
                BDCredito bdCredito = this.Infra.InstanciarBD<BDCredito>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdCredito.Incluir(toCredito);
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
        /// <param name="toCredito">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCredito toCredito)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCredito.CodCredito.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CREDITO"));
                }
                #endregion
                //TODO: regras de negócio
                BDCredito bdCredito = this.Infra.InstanciarBD<BDCredito>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdCredito.Alterar(toCredito);
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
        /// <param name="toCredito">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCredito toCredito)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCredito.CodCredito.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CREDITO"));
                }
                #endregion
                //TODO: regras de negócio
                BDCredito bdCredito = this.Infra.InstanciarBD<BDCredito>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdCredito.Excluir(toCredito);
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
        #endregion
    }
}
