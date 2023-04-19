using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>Tipos de falha para a RN de Conta</summary>
    public enum TipoFalha
    {
        /// <summary>Falha do campo Agencia</summary>
        AgenciaInvalida,
        /// <summary>Conta inválida</summary>
        ContaInvalida,
        /// <summary>Espécie inválida</summary>
        EspecieInvalida,
        /// <summary>Limite inválido</summary>
        LimiteInvalido,
        /// <summary>Saldo deve ser 0</summary>
        SaldoZero,
        /// <summary>Valor transação dever ser maior que zero</summary>
        ValorTransacao,
        ///<summary> Conta inexistente</summary>
        ContaInexistente,
        ///<summary> Saldo Insuficiente</summary>
        SaldoInsuficiente
    }

    /// <summary>Classe de mensagens para a RN de Conta</summary>
    public class MensagemConta : Mensagem
    {
        /// <summary>Cria uma mensagem para o usuário final</summary>
        /// <param name="tipoFalha">Tipo da falha</param>
        /// <param name="parametro">Parâmetros para a mensagem</param>
        public MensagemConta(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.AgenciaInvalida:
                    this.mensagem = "Agência inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.ContaInvalida:
                    this.mensagem = "Conta inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.EspecieInvalida:
                    this.mensagem = "Espécie inválida. Deve ser 35 para pessoa física e 06 para pessoa jurídica.";
                    break;
                case TipoFalha.LimiteInvalido:
                    this.mensagem = "Limite deve ser maior que zero.";
                    break;
                case TipoFalha.SaldoZero:
                    this.mensagem = "Saldo inicial deve ser zero.";
                    break;
                case TipoFalha.ValorTransacao:
                    this.mensagem = "Valor da transação deve ser maior que zero.";
                    break;
                case TipoFalha.ContaInexistente:
                    this.mensagem = "Conta Inexistente";
                    break;
                case TipoFalha.SaldoInsuficiente:
                    this.mensagem = String.Format("Saldo insuficiente. Total disponível para saque é {0} ", parametro[0]);
                    break;
                default:
                    break;
            }
        }
    }
}
