using Bergs.Pwx.Pwxoiexn;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcsocxn;
using Bergs.Pwx.Pwxoiexn.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bergs.Pwx.Pwxoiexn.Mensagens;

namespace Bergs.Pxc.Pxcs<identificador>xn
{
    internal class RegrasNegocios : AplicacaoRegraNegocio
    {
        /// <summary>RN01</summary>
        /// <param name="toOperacaoCred">TO referente à tabela PXC.OperacaoCred</param>
        /// <returns>Resposta da regra de negócio</returns>
        public override Retorno<int> RN04(TOOperacaoCred toOperacaoCred)
        {
            try
            {
                if (toOperacaoCred.DataInicioOperacaoCredito != null)
                {
                    return this.Infra.RetornarFalha<int>(new Mensagem(TipoMensagem.Erro, "Não é permitido alterar a Data de Início de uma Operação de Crédito."));
                }

                if (toOperacaoCred.DataFimOperacaoCredito != null)
                {
                    return this.Infra.RetornarFalha<int>(new Mensagem(TipoMensagem.Erro, "Não é permitido alterar a Data de Fim de uma Operação de Crédito."));
                }

                // Calcular valor da parcela
                decimal valorParcela = (toOperacaoCred.ValorFinanciado * (decimal)Math.Pow((1 - 0.011), toOperacaoCred.QuantidadeParcelas)) / toOperacaoCred.QuantidadeParcelas;

                return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem("Inclusão"));
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<int>(ex);
            }
        }
    }
}
================================================================================================
        /// <summary>
        /// RN01 - Validação de datas de operação de crédito
        /// </summary>
        /// <param name="toOperacaoCred">TO referente à tabela PXC.OperacaoCred</param>
        /// <returns>Resposta da regra de negócio</returns>
        public virtual Retorno<Int32> RN05(TOOperacaoCred toOperacaoCred)
        {
            try
            {
                // Se a Data de Início da Operação de Crédito foi informada
                if (toOperacaoCred.DataInicio.HasValue)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.Erro, "Não é permitido alterar a Data de Início de uma Operação de Crédito."));
                }

                // Se a Data de Fim da Operação de Crédito foi informada
                if (toOperacaoCred.DataFim.HasValue)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.Erro, "Não é permitido alterar a Data de Fim de uma Operação de Crédito."));
                }

                // Caso contrário, permite o cadastramento
                return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem("Inclusão"));
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
=================================================================================================================================================
        /// <summary>
        /// RN01
        /// </summary>
        /// <param name="toOperacaoCred">TO referente à tabela PXC.OperacaoCred</param>
        /// <returns>Resposta da regra de negócio</returns>
        public virtual Retorno<Int32> RN08(TOOperacaoCred toOperacaoCred)
        {
            try
            {
                if (toOperacaoCred.Situacao != SituacaoOperacaoCredito.CONTRATADA)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.ALERTA, "Operação de crédito não está em situação CONTRATADA."));
                }
                else if (toOperacaoCred.DataInicio == null)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.ALERTA, "Não é possível encerrar Operação de Crédito sem Data de Início."));
                }
                else if (toOperacaoCred.DataFim != null)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.ALERTA, "Não é permitido encerrar Operação de Crédito já encerrada."));
                }
                else
                {
                    toOperacaoCred.DataFim = DateTime.Now.Date;
                    return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem("Encerramento"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
=======================================================================================================================================================
        /// <summary>RN01</summary>
        /// <param name="toCOperacaoCred">TO referente à tabela PXC.OperacaoCred</param>
        /// <returns>Resposta da regra de negócio</returns>
        public virtual Retorno<Int32> RN01(TOOperacaoCred toOperacaoCred)
        {
            try
            {
                if (toOperacaoCred.Situacao != SituacaoOperacaoCredito.SOLICITADA)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.ERRO, "Só é permitido CONTRATAR operações de crédito com situação SOLICITADA."));
                }

                if (toOperacaoCred.DataInicio.HasValue)
                {
                    return this.Infra.RetornarFalha<Int32>(new Mensagem(TipoMensagem.ERRO, "Não é permitido contratar Operação de Crédito já contratada."));
                }
                else
                {
                    toOperacaoCred.DataInicio = DateTime.Now;
                    return this.Infra.RetornarSucesso(1, new OperacaoRealizadaMensagem("Contratação"));
                }
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<Int32>(ex);
            }
        }
=======================================================================================================================================================
        /// <summary>RF-01.6 Manter Operação de Crédito</summary>
        /// <param name="toOperacaoCred">TO referente à tabela PXC.OperacaoCred</param>
        /// <returns>Resposta da regra de negócio</returns>
        public virtual Retorno<int> RF_01_6(TOOperacaoCred toOperacaoCred)
        {
            try
            {
                if (toOperacaoCred.Situacao != SituacaoOperacaoCredito.SOLICITADA)
                {
                    return this.Infra.RetornarFalha<int>(new Mensagem(TipoMensagem.ERRO, "Não é permitido alterar Operação de Crédito com situação diferente de Solicitada."));
                }

                if (toOperacaoCred.DataInicio != null)
                {
                    return this.Infra.RetornarFalha<int>(new Mensagem(TipoMensagem.ERRO, "Não é permitido alterar Operação de Crédito já contratadas."));
                }

                // Se chegou aqui, a validação passou
                return this.Infra.RetornarSucesso<int>(0);
            }
            catch (Exception ex)
            {
                return this.Infra.TratarExcecao<int>(ex);
            }
        }