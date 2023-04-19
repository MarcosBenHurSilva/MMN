using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Tipos de falha para a RN de Cliente</summary>
    public enum TipoFalha
    {
        /// <summary>Falha de Cpf inválido</summary>
        CpfInvalido,
        /// <summary>Falha de Cnpj inválido</summary>
        CnpjInvalido,
        /// <summary>Falha de tipo pessoa inválido</summary>
        TipoPessoaInvalido,
        /// <summary>Nome da pessoa de ter 2 palavras</summary>
        NomePessoa
    }

    /// <summary>Classe de mensagens para a RN de Cliente</summary>
    public class MensagemCliente : Mensagem
    {
        /// <summary>Cria uma mensagem para o usuário final</summary>
        /// <param name="tipoFalha">Tipo da falha</param>
        /// <param name="parametro">Parâmetros para a mensagem</param>
        public MensagemCliente(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.CpfInvalido:
                    this.mensagem = "CPF inválido.";
                    break;
                case TipoFalha.CnpjInvalido:
                    this.mensagem = "CNPJ inválido.";
                    break;
                case TipoFalha.TipoPessoaInvalido:
                    this.mensagem = "Tipo pessoa deve ser 'F' ou 'J'.";
                    break;
                case TipoFalha.NomePessoa:
                    this.mensagem = "Nome deve ter 2 (dois) nomes e no mínimo 2 (duas) letras no primeiro nome.";
                    break;
                default:
                    break;
            }
        }
    }
}
