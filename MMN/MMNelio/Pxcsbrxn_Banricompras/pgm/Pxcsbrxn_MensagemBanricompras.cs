using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsbrxn
{
    /// <summary>Tipos de falha para a RN de Banricompras</summary>
    public enum TipoFalha
    {
        /// <summary></summary>
        ValorMenorZero,
        /// <summary></summary>
        TempoAtualizacaoRatingInvalido,
        /// <summary></summary>
        MovimentacaoInferior5000,
        /// <summary></summary>
        RatingA,
        /// <summary></summary>
        TipoPessoaInvalido
    }

    /// <summary>Classe de mensagens para a RN de Banricompras</summary>
    public class MensagemBanricompras : Mensagem
    {
        public MensagemBanricompras(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.ValorMenorZero:
                    this.mensagem = string.Format("O valor do campo {0} deve ser maior do que zero.", parametro[0]);
                    break;
                case TipoFalha.TempoAtualizacaoRatingInvalido:
                    this.mensagem =
                        string.Format(
    @"Para atualizar o rating, ele deve ter sido atualizado há pelo menos um mês. A data de última atualização foi {0}.", parametro[0]);
                    break;
                case TipoFalha.MovimentacaoInferior5000:
                    this.mensagem = string.Format(
                        "Para atualizar o rating, a movimentação nos últimos 30 dias deve ser superior a R$ 5.000,00. A movimentação foi {0}.", parametro[0]);
                    break;
                case TipoFalha.RatingA:
                    this.mensagem = "O rating do cliente é A e não será atualizado";
                    break;
                case TipoFalha.TipoPessoaInvalido:
                    this.mensagem = "Operação disponível apenas para Pessoa Jurídica";
                    break;
                default:
                    break;
            }
        }
    }
}
